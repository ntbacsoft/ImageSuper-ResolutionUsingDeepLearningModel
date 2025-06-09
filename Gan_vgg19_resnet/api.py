import os 
import subprocess
from flask import Flask, request, jsonify, send_file
from skimage.metrics import structural_similarity as ssim
from scale import *
import numpy as np
import cv2

app = Flask(__name__)
UPLOAD_FOLDER = "uploads"
DOWNLOAD_FOLDER = "result"

os.makedirs(UPLOAD_FOLDER, exist_ok=True)
os.makedirs(DOWNLOAD_FOLDER, exist_ok=True)


@app.route('/upload', methods=['POST'])
def upload_file():
    clear_folder(UPLOAD_FOLDER)
    clear_folder(DOWNLOAD_FOLDER)
    if 'file' not in request.files:
        return jsonify({'error': 'No file part'}), 400
    file = request.files['file']
    if file.filename == '':
        return jsonify({'error': 'No selected file'}), 400
    filepath = os.path.join(UPLOAD_FOLDER, file.filename)
    file.save(filepath)
    scale_image(UPLOAD_FOLDER, file.filename)
    print("ai processing")
    try:
        command = [
            "python", "main.py",
            "--mode", "test_only",
            "--LR_path", "./uploads",
            "--generator_path", "model/gene_1500.pt"
        ]
        result = subprocess.run(command, capture_output=True, text=True)

        if result.returncode != 0:
            return jsonify({'error': 'Error while running the model', 'details': result.stderr}), 500
        print("processing successfully")
        return jsonify({'message': 'File uploaded and processed successfully', 'filename': file.filename})
    
    except Exception as e:
        return jsonify({'error': str(e)}), 500
 
@app.route('/download', methods=['GET'])
def list_Upfiles():
    try:
        files = os.listdir(UPLOAD_FOLDER)
        return jsonify(files)
    except Exception as e:
        return jsonify({'error': str(e)}), 500
@app.route('/download/<filename>', methods=['GET'])
def download_file(filename):
    try:
        file_path = os.path.join(UPLOAD_FOLDER, filename)
        if not os.path.exists(file_path):
            return jsonify({'error': 'File not found'}), 404
        return send_file(file_path, mimetype='image/jpeg')
    except Exception as e:
        return jsonify({'error': str(e)}), 500

@app.route('/result', methods=['GET'])
def list_files():
    try:
        files = os.listdir(DOWNLOAD_FOLDER)
        return jsonify(files)
    except Exception as e:
        return jsonify({'error': str(e)}), 500
@app.route('/result/<filename>', methods=['GET'])
def result_file(filename):
    try:
        file_path = os.path.join(DOWNLOAD_FOLDER, filename)
        if not os.path.exists(file_path):
            return jsonify({'error': 'File not found'}), 404
        return send_file(file_path, mimetype='image/jpeg')
    except Exception as e:
        return jsonify({'error': str(e)}), 500
@app.route('/psnr', methods=['GET'])
def get_psnr_auto():
    try:
        files_upload = os.listdir(UPLOAD_FOLDER)
        files_result = os.listdir(DOWNLOAD_FOLDER)

        if not files_upload or not files_result:
            return jsonify({'error': 'Missing image(s) in one of the folders'}), 400

        img1_path = os.path.join(UPLOAD_FOLDER, files_upload[0])
        img2_path = os.path.join(DOWNLOAD_FOLDER, files_result[0])

        img1 = cv2.imread(img1_path)
        img2 = cv2.imread(img2_path)

        if img1 is None or img2 is None:
            return jsonify({'error': 'Could not read image(s)'}), 400

        if img1.shape != img2.shape:
            img2 = cv2.resize(img2, (img1.shape[1], img1.shape[0]))

        psnr_value = calculate_psnr(img1, img2)
        ssim_value = calculate_ssim(img1, img2)
        return jsonify({
            'psnr': psnr_value,
            'ssim': ssim_value,
        })
    except Exception as e:
        return jsonify({'error': str(e)}), 500



def clear_folder(folderPath):
    if not os.path.exists(folderPath):
        os.makedirs(folderPath)
    for filename in os.listdir(folderPath):
        file_path = os.path.join(folderPath, filename)
        if os.path.isfile(file_path):
            try:
                os.remove(file_path)
            except Exception as e:
                print(f"Failed to delete {file_path}: {e}")



def calculate_psnr(img1, img2):
    mse = np.mean((img1 - img2) ** 2)
    if mse == 0:
        return float('inf')
    PIXEL_MAX = 255.0
    return 20 * np.log10(PIXEL_MAX / np.sqrt(mse))

def calculate_ssim(img1, img2):
    gray1 = cv2.cvtColor(img1, cv2.COLOR_BGR2GRAY)
    gray2 = cv2.cvtColor(img2, cv2.COLOR_BGR2GRAY)
    score, _ = ssim(gray1, gray2, full=True)
    return score


if __name__ == '__main__':
    app.run(debug=True, port=5000)