#include "QtFrameDesignModel.h"
#include <QtWidgets/QApplication>


int main(int argc, char *argv[])
{
	QApplication a(argc, argv);
	//获取版本号测试
	/*if (argc < 2)
    {
		fprintf(stdout,"%s Version %d.%d\n",
            argv[0],
            Tutorial_VERSION_MAJOR,
            Tutorial_VERSION_MINOR);
		fprintf(stdout,"Usage: %s number\n",argv[0]);
		return 1;
    }*/

	QtFrameDesignModel w;
	w.show();
	return a.exec();
}
