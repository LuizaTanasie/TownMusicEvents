function neighborhood = getRecommendations_test(fanIdx,k,MS)
    pears = corr(MS','Type','Pearson');
    [pears,fansIdx]=sort(pears(:,fanIdx),'descend'); % [pearson score, fan index]
    [L,M]=find(isnan(pears));
    if (~isempty(L))
        pears(L,M)=0;   %get rid of NaN
    end
    [pears,fansIdx]=sort(pears,'descend');
    neighborhood=full([pears,fansIdx]);
    neighborhood(1,:)=[];
    neighborhood = neighborhood(1:k,:);
    
