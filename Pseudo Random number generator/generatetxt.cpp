#include "generatetxt.h"

Generatetxt::Generatetxt()
{

}

QChar Generatetxt::generateRandomChar()
{
    QChar ret;
    int index = nogen.generate(charList.size());
    ret = charList.at(index);
    return ret;
}


QString Generatetxt::generatePass(int size)
{
    QString ret;
    for(int i = 0;i<size;i++)
    {
        ret.append(generateRandomChar());
    }
    return ret;
}
