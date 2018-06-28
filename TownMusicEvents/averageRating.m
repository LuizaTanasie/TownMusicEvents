function avgRatingsFan=averageRating(MS,fanIdx)   
    avgRatingsFan=0;
    noOfRatingsFan=nnz(MS(fanIdx,:));
    if noOfRatingsFan~=0
        for i=1:noOfRatingsFan
            avgRatingsFan=avgRatingsFan+MS(fanIdx,i);
        end
        avgRatingsFan = avgRatingsFan/noOfRatingsFan;
    end