#include <iostream>
#include <cmath>
#include <vector>

using namespace std;
class Point{
private:
    float x,y;
public:
    void setX(float _x){
        x=_x;
    };
    void setY(float _y){
        y=_y;
    };
    float getX() const{
        return x;
    };
    float getY() const{
        return y;
    };

    Point(){
        x=0;
        y=0;
        cout << "set zero" << endl;
    };
    Point(float _x, float _y){
        x =_x;
        y=_y;
    };
    Point(int _x, int _y){
        x =_x;
        y=_y;
    };
    Point(const Point &t){
        x=t.getX(); y=t.getY();
    };
    void operator=(const Point &t){
        x=t.getX(); y=t.getY();
    };
    float ToZero(){
        return sqrt(pow(x,2)+pow(y,2));
    };
  /*  ~Point(){                                           // деструктор
        cout<<"death"<<endl;
    }*/
};

class Broken{
    friend class Loop_Broken;
private:
    vector<Point> v;
    float perimetr = 0.0;
    bool isLock;
public:
    Broken(){
        cout << "set zero" << endl;
    };
    Broken(Point m[],int k){                            // констуктор
        for(int i=0;i<k;i++)
            v.push_back(m[i]);
    };
    Broken(vector<Point> &m){
        v=m;
    };
    Broken(Broken &p){
        v=p.v;
    };
   void get() {
       for (int i =0; i < v.size(); i++) {
           cout<<v[i].getX()<<" "<<v[i].getY()<<endl;
       }

   };
    void operator=(const Broken &t){
        v = t.v;
    };
    virtual float perm(){
        for(int i = 0; i < v.size() - 1;i++){
        perimetr = perimetr + sqrt(pow(v[i+1].getX() - v[i].getX(), 2) + pow(v[i+1].getY() - v[i].getY(), 2));
        }
        cout << perimetr << endl;
        return perimetr;
    };
    /* ~Broken(){                                                  // деструктор
        cout<<"death1"<<endl;
    };*/
};

class Loop_Broken : public Broken{

public:
    Loop_Broken(){
        cout << "set zero" << endl;
    };
    Loop_Broken(Point m[], int k):Broken(m, k){}

    virtual float perm(){
        float t = Broken::perm();
        t += sqrt(pow(v[0].getX() - v[v.size() - 1].getX(), 2) + pow(v[0].getY() - v[v.size() - 1].getY(), 2));
        cout << t;
        return t;
      
    };
    Loop_Broken(vector<Point> &m):Broken(m){}
    Loop_Broken(Broken &p):Broken(p){}
    void operator=(const Loop_Broken &t){
    v = t.v;
    }
};

class Polygon{
private:
    vector<Point> v;
    float perimetr = 0.0;
    float s = 0.0;
public:
    Polygon(){
        cout << "set zero" << endl;
    };
    Polygon(Point m[],int k){                            // констуктор
        for(int i=0;i<k;i++)
            v.push_back(m[i]);
        
            if (k == 3){
                cout << "эт треугольник" << endl;
            }
            if (k == 4){
                cout << "эт 4угольник" << endl;
            }
            if (k > 4){
                cout << "это многоугольник" << endl;
        }
    };
    Polygon(vector<Point> &m){
        v=m;
            if (v.size() == 3){
                cout << "эт треугольник" << endl;
            }
            if (v.size() == 4){
                cout << "эт 4угольник" << endl;
            }
            if (v.size() > 4){
                cout << "это многоугольник" << endl;
            }

    };
    Polygon(Polygon &p){
        v=p.v;
            if (v.size() == 3){
                cout << "эт треугольник" << endl;
            }
            if (v.size() == 4){
                cout << "эт 4угольник" << endl;
            }
            if (v.size() > 4){
                cout << "это многоугольник" << endl;
            }
    };
   void get() {
       for (int i =0; i < v.size(); i++) {
           cout<<v[i].getX()<<" "<<v[i].getY()<<endl;
       }

   };
    void operator=(const Polygon &t){                
        v = t.v;
    };
    float perm(){
        for (int i = 0; i < v.size() - 1; i++){
            perimetr = perimetr + sqrt(pow(v[i + 1].getX() - v[i].getX(), 2) + pow(v[i + 1].getY() - v[i].getY(), 2));
            perimetr += sqrt(pow(v[0].getX() - v[v.size() - 1].getX(), 2) + pow(v[0].getY() - v[v.size() - 1].getY(), 2));
        }
        cout << perimetr << endl;
        return perimetr;
    }
    
    float area(){
            for (int i = 0; i < v.size() - 1; i++){
                s = s + 0.5 * abs((v[i].getX() + v[i + 1].getX())*(v[i].getY() - v[i + 1].getY()));
            }
        cout << s;
        return s;
        };
};

class Triangle : public Polygon{
public:
    Triangle(){
        cout << "set zero" << endl;
    };
    Triangle(Point m[],int k):Polygon(m,k){}
    Triangle(vector<Point> &m):Polygon(m){}
    Triangle(Polygon &p):Polygon(p){}
};

class Trapezoid : public Polygon{
public:
    Trapezoid(){
        cout << "set zero" << endl;
    };
    Trapezoid(Point m[],int k):Polygon(m,k){}
    Trapezoid(vector<Point> &m):Polygon(m){}
    Trapezoid(Polygon &p):Polygon(p){}
};

class RegularPolygon : public Polygon{
public:
    RegularPolygon(){
        cout << "set zero" << endl;
    };
    RegularPolygon(Point m[],int k):Polygon(m,k){
    }
    RegularPolygon(vector<Point> &m):Polygon(m){}
    RegularPolygon(Polygon &p):Polygon(p){}
};


int main(int argc, char** argv) {
    Point a;
    cout<<a.getX()<<" "<<a.getY()<<endl;
    Point b((float)3,(float)4);
    cout<<b.getX()<<" "<<b.getY()<<endl;
    Point c(b);
    cout<<c.getX()<<" "<<c.getY()<<endl;
    Point d=b;
    cout<<d.getX()<<" "<<d.getY()<<endl;
    cout<<d.ToZero()<<endl;
    
    
    Point a1 ((float)10.3, (float)11.3);
    Point b1 ((float)10.3, (float)12.3);
    Point c1 ((float)10.3, (float)13.3);
    Point d1 ((float)10.3, (float)14.3);
    Point e1((float)10.3, (float)15.3);
    
    Point arr[5] = {a1, b1, c1, d1, e1};
    Broken anime(arr, 5);
    anime.get();
    anime.perm();
    
    Loop_Broken v(arr, 5);
    v.perm();
    
    
    
    vector<Point> arr1{
        {1, 1},
        {5, 4},
        {9, 1}
    };
    Triangle T(arr1);
    T.perm();
    
    
    vector<Point> arr2{
        {1, 1},
        {4, 5},
        {10, 5},
        {13, 1}
    };
    Trapezoid h(arr2);
    h.perm();
    
    Polygon *t[2];
    t[0] = new Triangle();
    t[1] = new Trapezoid();
    t[0]->get();
    t[1]->get();
    return 0;
}
