function ppfc = PPFC(fanIdx,artistIdx,neighborhood,MS)
        
    avgRatingsFan=0;
    for i=1:nnz(MS(fanIdx,:))
        avgRatingsFan=avgRatingsFan+MS(fanIdx,i);
    end
    avgRatingsFan = avgRatingsFan/nnz(MS(fanIdx,:));
    
    avgRatingsArtist=0;
    for i=1:nnz(MS(:,artistIdx))
        avgRatingsArtist=avgRatingsArtist+MS(i,artistIdx);
    end
    avgRatingsArtist = avgRatingsArtist/nnz(MS(:,artistIdx));
    mySum = 0;
    mySum2=0;
    
    for i=1:length(neighborhood)
        currentNeighborIdx=neighborhood(i,2);
        if(MS(currentNeighborIdx,artistIdx)~=0)
            mySum=mySum+neighborhood(i,1)*(MS(neighborhood(i,2),artistIdx)-avgRatingsArtist);
            mySum2=mySum2+abs(neighborhood(i,1));
        end
    end
    if (mySum2~=0)
    ppfc=avgRatingsFan+(mySum)/mySum2;
    else
        ppfc=avgRatingsFan;
    end
    
    ppfc=full(ppfc);
    