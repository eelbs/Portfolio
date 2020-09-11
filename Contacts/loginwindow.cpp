#include "loginwindow.h"
#include "ui_loginwindow.h"



LoginWindow::LoginWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::LoginWindow)
{
    ui->setupUi(this);

    ui->tabWidget->setTabText(0,"Login");
    ui->tabWidget->setTabText(1,"Options");
    ui->tabWidget->setCurrentIndex(0);
    ui->HostEdit->setText("localhost");
    ui->DatabaseEdit->setText("poeple");
    this->setWindowTitle("Login");

    this->setWindowIcon(QIcon(":/Icons/Icons/Programicon.png"));
    mainwindow = new MainWindow();

    ui->UserName->setText("postgres");
    ui->PasswordEdit->setText("2501");

    connect(mainwindow,SIGNAL(sig(Contact)),this,SLOT(slurt(Contact)));
    connect(this,SIGNAL(createContact(Contact)),mainwindow,SLOT(addContact(Contact)));
    connect(mainwindow,SIGNAL(deleteAll()),this,SLOT(deleteContacts()));
    connect(mainwindow,SIGNAL(addContacts(Contact,int)),this,SLOT(addContact(Contact,int)));
    connect(mainwindow,SIGNAL(hideui()),this,SLOT(showlogin()));





    //set up database

    //SQLite
//    db = QSqlDatabase::addDatabase("QSQLITE");

//    db.setDatabaseName(path+dbname);
//    db.open();
//    if(!db.open())

//    {

//        QMessageBox msg;
//        msg.setText("Database connection fail");
//        msg.exec();
//    }
//    else
//    {

//    }
    db = QSqlDatabase::addDatabase("QPSQL");


    //DROP TABLE

//    QSqlQuery query;
//    query.prepare("DROP TABLE Contacts");
//    query.exec();


    //CREATE NEW TABLE


}

LoginWindow::~LoginWindow()
{
    delete ui;
}


void LoginWindow::on_LoginButton_clicked()
{
//    QMessageBox msg;
//    msg.setWindowTitle("Password");
//    msg.setText(ui->PasswordEdit->text());
//    msg.exec();


    //Check if username exists in database
    //if exists check hash and salt
    //it exsists and has and salt exists is correct log in

    db.setHostName(ui->HostEdit->text());
    db.setDatabaseName(ui->DatabaseEdit->text());
    db.setUserName(ui->UserName->text());
    db.setPassword(ui->PasswordEdit->text());
    if(db.open())
    {

        QSqlQuery query;

        query.prepare("\\c "+ui->DatabaseEdit->text());
        query.exec();
        query.prepare("CREATE TABLE IF NOT EXISTS Contacts (id INTEGER PRIMARY KEY, firstname TEXT,lastname TEXT,dd INTEGER,mm INTEGER,yyyy INTEGER,phone TEXT,email TEXT);");
        if(!query.exec())
        {
        }

        query.prepare("SELECT * FROM Contacts");

        if(query.exec())
        {
            while(query.next())
            {

                //int id = query.value(0).toInt();
                QString name = query.value(1).toString();
                QString surname = query.value(2).toString();
                int day = query.value(3).toInt();
                int month = query.value(4).toInt();
                int year = query.value(5).toInt();
                int phone = query.value(6).toInt();
                QString email = query.value(7).toString();
                QDate bday(year,month,day);
                Contact a(name,surname,bday,phone,email);
                emit(createContact(a));

            }
        }

        this->hide();
        mainwindow->show();
    }
    else
    {
        QMessageBox msg;
        msg.setText("Failed to connect to database");
        msg.exec();
    }




//    QMessageBox msg;
//    msg.setText(QString::number(qHash("root")));
//    msg.exec();

//    if((ui->UserName->text()=="root") && ( qHash("root")==qHash(ui->PasswordEdit->text()) ) )
//    {
//        this->hide();

//        mainwindow->show();
//    }
//    if((ui->UserName->text()!="root" || ui->PasswordEdit->text()!="root") && loginAttempts>0) //check credentials
//    {
//        QMessageBox msg;
//        msg.setText("Incorect credentials please try again. Attempts left "+ QString::number(loginAttempts));
//        msg.exec();
//        loginAttempts--;

//    }
//    if(loginAttempts<=0)
//    {
//        QMessageBox msg;
//        msg.setText("Too many incorrect attempts");
//        msg.exec();
//    }


    //Send username and pass to MainWindow class with signal and slot

    //Delete pass after sending signal.
}


void LoginWindow::slurt(Contact contact)
{
    QMessageBox msg;
    int id = mainwindow->getNoContacts();
    QString name = contact.getName();
    QString sur = contact.getSurname();
    QDate bday = contact.getBirthday();
    int dd = bday.day();
    int mm = bday.month();
    int yyyy = bday.year();
    int phone = contact.getPhone();
    QString email = contact.getEmail();
    QSqlQuery query;
    //query.prepare("CREATE TABLE IF NOT EXISTS Contacts (id INTEGER PRIMARY KEY, firstname TEXT,lastname TEXT,dd INTEGER,mm INTEGER,yyyy INTEGER,phone TEXT);"
    query.prepare("INSERT INTO Contacts (id,firstname,lastname,dd,mm,yyyy,phone,email) VALUES (:id,:firstname,:lastname,:dd,:mm,:yyyy,:phone,:email) ");
    query.bindValue(":id",id);
    query.bindValue(":firstname",name);
    query.bindValue(":lastname",sur);
    query.bindValue(":dd",dd);
    query.bindValue(":mm",mm);
    query.bindValue(":yyyy",yyyy);
    query.bindValue(":phone",phone);
    query.bindValue(":email",email);
    if(!query.exec())
    {

        QMessageBox msg;

        msg.setText("Failed to add contact to database" );
        msg.exec();
    }

    //testing

}

void LoginWindow::deleteContacts()
{
    QSqlQuery query;
    query.prepare("DELETE FROM Contacts");
    query.exec();
}

void LoginWindow::addContact(Contact contact, int id)
{
    QString name = contact.getName();
    QString sur = contact.getSurname();
    QDate bday = contact.getBirthday();
    int phone = contact.getPhone();
    int dd = bday.day();
    int mm = bday.month();
    int yyyy = bday.year();
    QString email = contact.getEmail();
    QSqlQuery query(db);

    query.prepare("INSERT INTO Contacts (id,firstname,lastname,dd,mm,yyyy,phone,email) VALUES (:id,:firstname,:lastname,:dd,:mm,:yyyy,:phone,:email) ");
    query.bindValue(":id",id);
    query.bindValue(":firstname",name);
    query.bindValue(":lastname",sur);
    query.bindValue(":dd",dd);
    query.bindValue(":mm",mm);
    query.bindValue(":yyyy",yyyy);
    query.bindValue(":phone",phone);
    query.bindValue(":email",email);
    query.exec();

}

void LoginWindow::showlogin()
{
    this->show();
}


void LoginWindow::on_EditOptionsButton_clicked()
{

}

void LoginWindow::on_CreateDatabaseButton_clicked()
{
    QSqlQuery query;
    query.prepare("CREATE DATABASE "+ui->DatabaseEdit->text());
    if(!query.exec())
    {
        QMessageBox msg;
        msg.setText("Failed to create database");
        msg.exec();
    }
}
