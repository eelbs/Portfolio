#ifndef GENERATETXT_H
#define GENERATETXT_H
#include <QString>
#include <QChar>
#include "generateno.h"


class Generatetxt
{
private:
    //This character list include the alphabet twice. this is to get more natural looking and memoralbe passwords by special characters less likley to be choosen.
    QString charList = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~`!@#$%^&*()_-+={}[]:;\"'\\,<.>/?";
    GenerateNo nogen;
public:
    Generatetxt();
    QChar generateRandomChar();
    QString generatePass(int size);

};

#endif // GENERATETXT_H
