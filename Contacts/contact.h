#ifndef CONTACT_H
#define CONTACT_H

#include <QString>
#include <QDate>
#include <QStringList>


class Contact
{
private:
    QString name;
    QString surname;
    QDate birthday;
    int phone;
    QString email;
public:
    Contact();
    Contact(QString Name, QString Surname, QDate Birthday , int Phone, QString Email);
    QString toString();


    const QString getName();
    const QString getSurname();
    const QDate getBirthday();
    int getPhone();
    const QString getEmail();

    void setName(QString);
    void setSurname(QString);
    void setDate(QDate);
    void setPhone(int);
    void setEmail(QString);


};

#endif // CONTACT_H
