#pragma once

#include <QtWidgets/QWidget>
#include "ui_QtFrameDesignModel.h"


class QtFrameDesignModel : public QWidget
{
	Q_OBJECT

public:
	QtFrameDesignModel(QWidget *parent = Q_NULLPTR);

private:
	Ui::QtFrameDesignModelClass ui;
};
