What does the program do?

- Displays a scripture to the user slowly removing words

What user inputs does it have?

- continue to hide words
- exit program

What output does it produce?

- the scripture
- slightly less of said scripture

How does the program end?

- user quits
- entire scripture hidden

# Determining Classes

What are good candidates for classes in this program?
What are the primary responsibilities of each class?

What attributes does this class need to fulfill its behaviors? (In other words, what variables should this class store?)
What are the data types of these member variables?
What constructors should each class have?

### Scripture

- \_contents (<list> of Words)?
- \_reference (a Reference)
- determine

> Display
> HideWords
> SetReference

### Reference

- \_contents (string)

> Display
> GetReference

#### Reference constructors

    public Reference(string bookName, int chapter, int verse)
        would return $"{bookName} {chapter}: {verse}"

    public Reference(string bookName, int chapter, int verse, int verse2)
        would return $"{bookName} {chapter}: {verse} - {verse2}"

### Word

- \_word string
- Status (hidden = T/F) - boolean?

> Display if word is hidded write underscores otherwise write the string
> Hide updates hidden attribute to true

---

####

- extra mile = only deleting words still present [deconstructor?]

# Functional Requirements

Your program must do the following:

    Store a scripture, including both the reference (for example "John 3:16") and the text of the scripture.
    Accommodate scriptures with multiple verses, such as "Proverbs 3:5-6".
    Clear the console screen and display the complete scripture, including the reference and the text.
    Prompt the user to press the enter key or type quit.
    If the user types quit, the program should end.
    If the user presses the enter key (without typing quit), the program should hide a few random words in the scripture, clear the console screen, and display the scripture again.
    The program should continue prompting the user and hiding more words until all words in the scripture are hidden.
    When all words in the scripture are hidden, the program should end.
    When selecting the random words to hide, for the core requirements, you can select any word at random, even if the word was already hidden. (As a stretch challenge, try to randomly select from only those words that are not already hidden.)

# Design Requirements

In addition your program must:

    Use the principles of Encapsulation, including proper use of classes, methods, public/private access modifiers, and follow good style throughout.
    Contain at least 3 classes in addition to the Program class: one for the scripture itself, one for the reference (for example "John 3:16"), and to represent a word in the scripture.
    Provide multiple constructors for the scripture reference to handle the case of a single verse and a verse range ("Proverbs 3:5" or "Proverbs 3:5-6").
