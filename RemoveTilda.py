#Updated 12-2-19 by Jareth Dodson
with open(r'C:\Users\Jareth\Downloads\sr28asc\FOOD_DES.txt', 'r') as infile:
    data = infile.read()
    data = data.replace("~", "")

with open(r'C:\Users\Jareth\Downloads\sr28asc\FOOD_DES.txt', 'w') as outfile:
    outfile.write(data)        
