%sa se genereze un spline cubic parametric care sa treaca prin niste puncte date. pctele se citesc cu ginput

function puncte()
    xt=[];
    yt=[];
    clf;
    axis([0 2 0 1]);
    hold on;
    set(gcf, 'currentchar','1')
    while get(gcf, 'currentchar')=='1'
        [x,y]=ginput(1);
        plot(x,y,'*bl');
        xt=[xt,x]; 
        yt=[yt,y];
    end
    tt=0:0.001:1;
    t=linspace(0,1,length(xt));
    [a,b,c,d] = Splinecubic(t,xt,2);
    sx=evalspline(t,a,b,c,d,tt);
    t=linspace(0,1,length(yt));
    [a,b,c,d] = Splinecubic(t,yt,2);
    sy=evalspline(t,a,b,c,d,tt);
    plot(sx,sy);