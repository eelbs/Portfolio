#ifndef MAINWINDOW_H
#define MAINWINDOW_H


#include <QList>
#include <QMainWindow>
#include <QStandardItem>
#include <QSettings>
#include "newcontact.h"
#include "contact.h"



#include <QMessageBox>

namespace Ui {
class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = nullptr);
    ~MainWindow();
    int getNoContacts();

private slots:
    void on_AddButton_clicked();

    void on_DeleteButton_clicked();

    void on_EditButton_clicked();

    void on_LogOutButton_clicked();

public slots:
    void Slurt(Contact contact);
    void addContact(Contact contact);
    void modifyContact(Contact contact,int index);


private:
    Ui::MainWindow *ui;
    NewContact * newContact;
    QString password; //To hold password temporarily. Remove data as soon as Database is opened.
    QList <Contact> Contacts;
    QStandardItemModel *itemModel;

    void resizeEvent(QResizeEvent * event);
    void closeEvent(QCloseEvent *event);
    void readSettings();

    void updateVeiw();
signals:
    void sig(Contact);
    void deleteAll();
    void addContacts(Contact,int);
    void hideui();

};

#endif // MAINWINDOW_H
