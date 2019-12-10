//#include <GL\glew.h>
//#include <GL\freeglut.h>
//#include <list>
//#include<random>
//
//using namespace std;
//
//static int w = 500, h = 500;
//bool isPerspective = true;
//
//void DrawTree(double scale = 0.5)
//{
//	glPushMatrix();
//
//	glScaled(scale, scale, scale);
//	glRotated(-90, 1, 0, 0);
//	glColor3d(0.9, 0.6, 0.3);
//	glutSolidCylinder(0.1, 0.3, 32, 32);
//
//	glTranslated(0, 0, 0.3);
//
//	glColor3d(0.0,96.0,35.0);
//	glutSolidCone(0.4, 0.3, 32, 32);
//	glTranslated(0, 0, 0.2);
//	glScaled(0.7, 0.7, 0.7);
//	glutSolidCone(0.4, 0.3, 32, 32);
//	glTranslated(0, 0, 0.2);
//	glScaled(0.7, 0.7, 0.7);
//	glutSolidCone(0.4, 0.3, 32, 32);
//
//	glPopMatrix();
//}
//
//void Render()
//{
//	x, z, scale
//	double trees[] = { 0.5, 0.5, 0.5 ,
//						   0.8, 0.8, 0.3,
//						   0.8, 0.3, 0.4, 
//						   0.1, 0.1, 0.2 ,
//						   0.9, 0.1, 0.3, 
//						   0.5, 0.1, 0.2, 
//						   0.7, 0.1, 0.2, 
//						   0.3, 0.1, 0.2 };
//	
//
//	glViewport(0, 0, w, h);
//	glMatrixMode(GL_PROJECTION);
//	glLoadIdentity();
//	if (isPerspective) 
//		gluPerspective(60, w * 1.0 / h, 0.1, 100);
//	else 
//		glOrtho(-1, 1, -1, 1, -100, 100);
//
//	glMatrixMode(GL_MODELVIEW);
//	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
//	glLoadIdentity();
//
//	glTranslated(0, 0, -1);
//	glRotated(20, 1, 0, 0);
//	glRotated(0, 0, 1, 0);
//	glTranslated(0.5, 0, 0.5);
//
//	for(int i = 0;i < 23; i+=3)
//	{
//		glPushMatrix();
//		glTranslated(-1 * trees[i], 0, -1 * trees[i+1]);
//		DrawTree(trees[i+2]);
//		glPopMatrix();
//	}
//
//	glFlush();
//	glutSwapBuffers();
//}
//
//int main(int argc, char** argv) {
//	glutInit(&argc, argv);
//	glutInitDisplayMode(GLUT_SINGLE);
//	glutInitWindowSize(500, 500);                    // window size
//	glutInitWindowPosition(100, 100);                // distance from the top-left screen
//	glutCreateWindow("Task 3");
//	glutDisplayFunc(Render);
//	glutMainLoop();
//}