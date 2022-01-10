def enterOutput(answer):
    if answer.lower() == "y":
        input("You are correct!")
    elif answer.lower() == "n":
        input("You are not correct.")
    elif answer.lower() == "i want more":
        input(
            "\nWhy this was made:\n\nBecause there is a secret, waiting for you, which you will discover someday.\n\nRemember, 6466 will help you.\n\nPress ENTER to exit..."
        )

    else:
        enterOutput(input("Please enter Y or N. "))


enterOutput(input("Do you think the earth is flat? (Y/N) "))
