function [mostSimilar]=genreSimilarity(userGenres)
    M = load('C:\Users\Luiza\Source\Repos\TME\TownMusicEvents\Recommender\genre-data.txt');
    ArtistIds = M(:,1);
    UniqueArtistIds = [unique(ArtistIds),[1:length(unique(ArtistIds))]']; %[artistId, index]
    for i=1:length(ArtistIds)
        rez=find(ArtistIds(i)==UniqueArtistIds(:,1));
        ArtistIds(i)=UniqueArtistIds(rez,2);
    end
    GenresIds = M(:,2);
    UniqueGenresIds = [unique(GenresIds),[1:length(unique(GenresIds))]'];
    for i=1:length(GenresIds)
        rez=find(GenresIds(i)==UniqueGenresIds(:,1));
        GenresIds(i)=UniqueGenresIds(rez,2);
    end
    Scores = M(:,3);
	userGenres=str2num(userGenres);
    MS = sparse(ArtistIds,GenresIds,Scores);
    sim=[;]; %[artistId,sim]
    for i=1:length(MS(:,1))
        vector = MS(i,:);
        sim(i,1) = UniqueArtistIds(i,1);
		userScores=userGenres(:,3);
        sim(i,2) = CosineSimilarity(vector,userScores');
    end
    [mostSimilar]=sortrows(sim,-2);

    
    
    
    
    
    