clear all; close all;
BJimg = imread('BAIJIN.PNG');
LHimg = imread('LANHEI.PNG');
 

%  k = 10;

 
for k = 1:200
 BJimg_R = double(BJimg(:,:,1));
 BJimg_G = double(BJimg(:,:,2));
 BJimg_B = double(BJimg(:,:,3));
 
 BJimg_R = BJimg_R(1:321,1:213);
 BJimg_G = BJimg_G(1:321,1:213);
 BJimg_B = BJimg_B(1:321,1:213);
 
 
 LHimg_R = double(LHimg(:,:,1));
 LHimg_G = double(LHimg(:,:,2));
 LHimg_B = double(LHimg(:,:,3));
 
  LHimg_R = LHimg_R(1:321,1:213);
  LHimg_G = LHimg_G(1:321,1:213);
  LHimg_B = LHimg_B(1:321,1:213);
 
 
  d_R = (BJimg_R - LHimg_R)./200;
  d_G = (BJimg_G - LHimg_G)./200;
   d_B = (BJimg_B - LHimg_B)./200;
 
   R = LHimg_R + k*d_R;
   G = LHimg_G + k*d_G;
   B = LHimg_B + k*d_B;
 
 img(:,:,1) = uint8(R); 
  img(:,:,2) = uint8(G);
   img(:,:,3) = uint8(B);
   
   imwrite(img, strcat('d', num2str( floor(k / 100)), num2str( floor(mod(k,100) / 10)), num2str(mod(k,10)), '.bmp'));
end