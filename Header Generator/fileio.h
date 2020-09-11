#ifndef FILEIO_H
#define FILEIO_H
#include <QString>
#include <QFileDialog>
#include <QFile>
#include <QTextStream>


class fileIO
{
public:
    fileIO();
    void SaveFile(QWidget * parent, QString text);
};

#endif // FILEIO_H
