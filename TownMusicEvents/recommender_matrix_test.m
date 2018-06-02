function recommender_matrix_test()
    M = load('F:\facultate\licenta\ydata250.txt');
    Fans = M(:,1);
    UniqueFans = [unique(Fans),[1:length(unique(Fans))]']; %[fanId, index]
    for i=1:length(Fans)
        rez=find(Fans(i)==UniqueFans(:,1));
        Fans(i)=UniqueFans(rez,2);
    end
    Artists = M(:,2);
    UniqueArtists = [unique(Artists),[1:length(unique(Artists))]'];
    for i=1:length(Artists)
        rez=find(Artists(i)==UniqueArtists(:,1));
        Artists(i)=UniqueArtists(rez,2);
    end
    Scores = M(:,3);
    MS = sparse(Fans,Artists,Scores);
    [I,J]=find(MS==255); %replace special value 255 with 1
    MS(I,J)=1;
    [I,J,S]=find(MS);
    NonZeroIdx=[I(:), J(:)]; %find nonzero indicies to select 20% random data from
    c=randperm(length(NonZeroIdx),80) ; 
    selectedIdx=NonZeroIdx(c,:); %[ i1,j1; i2,j2 ;...] 
    MSoriginal = MS;
    MS(selectedIdx(:,1),selectedIdx(:,2))=0; %replace test data with 0
    barData=[;];
    for i=1:length(UniqueFans)
        neighborhood = getRecommendations_test(i,100,MS); %get neighbrhood for fan
        [l,m]=find(selectedIdx(:,1)==i); %find if fan is in selected indexes
        if (~isempty(l))
            for j=1:length(l)
                ppfc=PPFC(selectedIdx(l(j),1),selectedIdx(l(j),2),neighborhood,MS);
                barData = [barData; [MSoriginal(selectedIdx(l(j),1),selectedIdx(l(j),2)),ppfc]];
            end
        end  
        end
       
   % barData= unique(barData,'rows');
    bar(barData)
  