#ifndef GENERATENO_H
#define GENERATENO_H
#include <QDateTime>

class GenerateNo
{
private:
    QDateTime now = QDateTime::currentDateTime();
    int var;
public:
    GenerateNo();
    int generate(int seed, int mod,int multi=2641452);
    int abs(int);
    int generate(int mod);
    int isEven(int);
};

#endif // GENERATENO_H
