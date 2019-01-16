function recommendedArtists = getRecommendations(fanId, k)
    [MS,UniqueFans,UniqueArtists]=recommender_matrix();
    fanIdx=find(UniqueFans(:,1)==fanId);
    neighborhood = getNeighborhood(fanIdx,k,MS);
    
    artistIdsToRecommend=[;]; %[artistId, ppfc]
    j=0;
    for i=1:length(UniqueArtists(:,1))
        if MS(fanIdx,UniqueArtists(i,2))==0
            ppfc=PPFC(fanIdx,UniqueArtists(i,2),neighborhood,MS);
            j=j+1;
            artistIdsToRecommend(j,1)=UniqueArtists(i,1); 
            artistIdsToRecommend(j,2)=ppfc;
        end
        
    end
    [recommendedArtists,I]=sortrows(artistIdsToRecommend,-2);
    
    
