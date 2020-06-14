"""
quentin-k 2020
"""
import os
import secrets

def clear():
    #clear the screen
    os.system('cls' if os.name == 'nt' else 'clear')

def genJwt():
    # generates a secure custom key
    jwt = secrets.token_hex(4)
    for _i in range(3):
        jwt += "-"
        jwt += secrets.token_hex(2)
    jwt += "-"
    jwt += secrets.token_hex(6)
    return jwt

# set email
email = input("What is your email? ")
clear()

# set email password
password = input("What is your email password? ")
clear()

# set smtp client
host = input("What is your smtp client (press enter for gmail)? ")
clear()
if host == "":
    host = "smtp.gmail.com"

# set smtp port
port = input("What port do you want to use (press enter for 587)?")
clear()
if port == "":
    port = "587"

# generate jwt key
customKey = input("type `custom key` to use a custom key: ")
clear()
if customKey == "custom key":
    jwt = input("What is your jwt key? ")
else:
    jwt = genJwt()
clear()
# configure dotnet secrets
dotnetMsg = "dotnet user-secrets set "
os.system("cd ./MoneyShare/")
os.system(dotnetMsg+'"Email: Username" "'+email+'"')
os.system(dotnetMsg+'"Email:Host" "'+host+'"')
os.system(dotnetMsg+'"Email:Password" "'+password+'"')
os.system(dotnetMsg+'"Email:Port"'+port+'"')
os.system(dotnetMsg + '"Email:From" "' + email + '"')
os.system(dotnetMsg + '"JwtKey" "' + jwt + '"')

print("Done")