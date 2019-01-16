function s = evalspline(x,a,b,c,d,X)
  for w = 1:length(X)
    index = 1;
    for i=1:length(x)-1
      if (X(w)>=x(i) && X(w)<=x(i+1))
        index = i;
        break;
      end
    end
    s(w) = a(index)*((X(w)-x(index))^3) + b(index)*((X(w)-x(index))^2) + c(index)*((X(w)-x(index))) + d(index);
  end
  