function [MS,UniqueFans,UniqueArtists]=recommender_matrix()
    M = load('C:\Users\Luiza\Source\Repos\TME\TownMusicEvents\Recommender\rating-data.txt');
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
