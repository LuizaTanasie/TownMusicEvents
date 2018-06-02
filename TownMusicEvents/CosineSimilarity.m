function [similarity] = CosineSimilarity(x1,x2)
if ( length (x1) == length(x2) )
    similarity = sum(x1.*x2) / (norm(x1) * norm(x2));
else
   disp('Vectors dimensions does  not match'); 
end