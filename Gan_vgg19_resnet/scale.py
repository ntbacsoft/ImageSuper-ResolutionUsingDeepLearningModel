from PIL import Image     
import os
def scale_image(upload, fileName):
    image_path = upload + "/" + fileName
    if(image_path.endswith(".jpg")):
        output_path = "./uploads/scaled_image.jpg"
    elif(image_path.endswith(".png")): 
        output_path = "./uploads/scaled_image.png"
    elif(image_path.endswith(".jpeg")):
        output_path = "./uploads/scaled_image.jpeg"
    elif(image_path.endswith(".webp")):
        output_path = "./uploads/scaled_image.webp"
    with Image.open(image_path).convert("RGB") as img:
        os.remove(image_path)
        original_width, original_height = img.size
        new_width = original_width // 4
        new_height = original_height // 4

        scaled_img = img.resize((new_width, new_height))

        scaled_img.save(output_path)

