#ifndef LOGINWINDOW_H
#define LOGINWINDOW_H

#include <QMainWindow>
#include "mainwindow.h"
#include <QMessageBox>
#include <QCryptographicHash>
#include <QSqlDatabase>
#include <QSqlQuery>


QT_BEGIN_NAMESPACE
namespace Ui { class LoginWindow; }
QT_END_NAMESPACE


class LoginWindow : public QMainWindow
{
    Q_OBJECT

public:
    LoginWindow(QWidget *parent = nullptr);
    ~LoginWindow();


private slots:

    void on_LoginButton_clicked();

    void on_EditOptionsButton_clicked();

    void on_CreateDatabaseButton_clicked();

public slots:
    void slurt(Contact);
    void deleteContacts();
    void addContact(Contact, int);
    void showlogin();
private:
    Ui::LoginWindow *ui;
    QString password;
    int passlen=0;
    MainWindow * mainwindow;
    int loginAttempts = 10;
    QSqlDatabase db;

signals:
    void createContact(Contact contact);



};
#endif // LOGINWINDOW_H
