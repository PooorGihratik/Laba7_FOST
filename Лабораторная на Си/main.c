#include <stdio.h>
#include <stdlib.h>
#include <errno.h>
#include <math.h>

FILE *fp;

int main(int argc, char *argv[]) {
	fp = fopen("C:\\Users\\Admin\\Desktop\\FOST\\data.txt", "w");
	if((fp = fopen("C:\\Users\\Admin\\Desktop\\FOST\\data.txt", "w"))== NULL){
		printf("%s", strerror(errno));
		exit(1);
	} else{
		printf("File is opened\n");
	}
	float x, y, t=0, v, angle;
	int tmp;
	const float g = 9.81;
	printf("Enter speed: ");
	scanf("%f", &v);
	printf("Input method of angle:\n1.In radians\n2.In degrees\n");
	do{
		scanf("%d", &tmp);
		if(tmp == 1){
			printf("Enter angle: ");
			scanf("%f", &angle);
			break;
		} else if(tmp == 2){
			printf("Enter angle: ");
			scanf("%f", &angle);
			angle = (M_PI * angle)/180;
			break;
		} else {
			printf("Incorrect value, please try again\n");
		}
	} while(1);
	do{
		t += 0.0005;
		x = v * cos(angle) * t;
		y = v * sin(angle) * t - g*t*t/2;
		fprintf(fp, "%f   %f\n", x, y);
	} while( y > 0);
	fclose(fp);
	return 0;
}
