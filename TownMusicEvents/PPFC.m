function ppfc = PPFC(fanIdx,artistIdx,neighborhood,MS)
        
    avgRatingsCurrentFan=averageRating(MS,fanIdx);
    mySum = 0;
    mySum2=0;
    
    for i=1:length(neighborhood)
        currentNeighborIdx=neighborhood(i,2);
        if(MS(currentNeighborIdx,artistIdx)~=0)
            mySum=mySum+neighborhood(i,1)*(MS(currentNeighborIdx,artistIdx)-averageRating(MS,currentNeighborIdx));
            mySum2=mySum2+abs(neighborhood(i,1));
        end
    end
    if (mySum2~=0)
    ppfc=avgRatingsCurrentFan+(mySum)/mySum2;
    else
        ppfc=avgRatingsCurrentFan;
    end
    
    ppfc=full(ppfc);
    if ppfc>5
        ppfc=5;
    end
    