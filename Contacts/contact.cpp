#include "contact.h"

Contact::Contact()
{

}
Contact::Contact(QString Name,QString Surname,QDate Birthday ,int Phone,QString Email)
{
    name = Name;
    surname = Surname;
    birthday = Birthday;
    phone = Phone;
    email = Email;
}

QString Contact::toString()
{

    QStringList list;

    //Convert members to QString and append to QStringlist

    list.append(name);
    list.append(surname);
    list.append(birthday.toString());
    list.append(QString::number(phone));

    QString ret = "";
    foreach (QString str, list)
    {
        ret.append(str);
        ret.append(" ");
    }
    return ret;

}

const QString Contact::getName()
{
    return name;
}
const QString Contact::getSurname()
{
    return surname;
}
const QDate Contact::getBirthday()
{
    return birthday;
}
int Contact::getPhone()
{
    return phone;
}

const QString Contact::getEmail()
{
    return email;
}


void Contact::setName(QString Name)
{
    name = Name;
}
void Contact::setSurname(QString Surname)
{
    surname = Surname;
}
void Contact::setDate(QDate Bday)
{
    birthday = Bday;
}
void Contact::setPhone(int Phone)
{
    phone = Phone;
}

void Contact::setEmail(QString Email)
{
    email = Email;
}
