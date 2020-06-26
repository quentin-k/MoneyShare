"""
quentin-k 2020
"""
from os import name as n
from os import system as s
from secrets import token_hex as t

def clear():
    #clear the screen
    s('cls' if n == 'nt' else 'clear')

def genJwt():
    # generates a secure custom key
    jwt = t(4)
    for _i in range(3):
        jwt += "-"
        jwt += t(2)
    jwt += "-"
    jwt += t(6)
    return jwt

# set email
email = input("What is your email? ")
clear()

# set email from
email_from = input("What is your desired from email (" + email + ") is default: ")
clear()
if email_from == "":
    email_from = email

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
s("dotnet secrets init")
dotnetMsg = "dotnet user-secrets set "
s("cd ./MoneyShare/")
s(dotnetMsg+'"Email:Username" "'+email+'"')
s(dotnetMsg+'"Email:Host" "'+host+'"')
s(dotnetMsg+'"Email:Password" "'+password+'"')
s(dotnetMsg+'"Email:Port" "'+port+'"')
s(dotnetMsg + '"Email:From" "' + email_from + '"')
s(dotnetMsg + '"JwtKey" "' + jwt + '"')

print("Done")