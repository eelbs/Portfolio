#ifndef GENERATEHEADER_H
#define GENERATEHEADER_H

#include <QString>
#include <QStringList>

//Add var names, types, and generate getters and setters.
//All output headertext as a qstring with a function.
class GenerateHeader
{
private:
    QString pub; //public text to return.
    QString priv ; //private text to return.
    QString capFirst(QString);  //return argument with first letter capitalized.
public:
    GenerateHeader();
    const QString getFileText();
    void add(QString type , QString name , QString parameterName = "i");

};

#endif // GENERATEHEADER_H
