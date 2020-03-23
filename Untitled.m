x = imread('cat.jpg');
%imshow(x);%
A=imresize(x,2);
%imshow(A);%
angle=180;
B=imrotate(A,angle);
%imshow(B);%
C=imcrop(A,[120 100 120 100]);
imshow(C);