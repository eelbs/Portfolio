#ifndef NEWCONTACT_H
#define NEWCONTACT_H

#include <QWidget>
#include "contact.h"


namespace Ui {
class NewContact;
}

class NewContact : public QWidget
{
    Q_OBJECT

public:
    explicit NewContact(QWidget *parent = nullptr);
    ~NewContact();
    void EditMode(Contact contact,int index);
    void CreateMode();


private slots:
    void on_AddButton_clicked();

private:
    Ui::NewContact *ui;
    bool editMode;
    int editModeIndex;
signals:
    void Sig(Contact);
    void ModContact(Contact,int);
};

#endif // NEWCONTACT_H
