//
//  main.cpp
//  2lab_oop
//
//  Created by pavel on 15.03.2021.
//

#include <iostream>
#include <ostream>
#include <vector>
using namespace std;

class monomial{
protected:
    float c;
    int degree;
public:
    monomial(){};

    float get_c(){
        return c;
    }
    int get_d(){
        return degree;
    }
    
    void set_c(float c_){
        c = c_;
    }
    void set_d(int degree_){
        degree = degree_;
    }
    
    monomial(float c_, int degree_){
        c = c_;
        degree = degree_;
    }
};

class Polynom{
private:
    vector<monomial> p;
public:
    Polynom(){}
    
    void set(monomial &o){
        for(int i = 0; i < p.size(); i++){
            if(p[i].get_d() == o.get_d()){
                p[i].set_c(p[i].get_c() + o.get_c());
                if(p[i].get_c() == 0) p.erase(p.begin() + i);
                return;
            }
        }
        p.push_back(o);
    }
    
    Polynom(vector <monomial> const _p): p(_p){};
    
    Polynom(int n, monomial arr[]){
        for(size_t i = 0; i < n; i++){
            p.push_back(arr[i]);
        }
    }
    
    Polynom(Polynom const &a){
        p = a.p;
    }
    
    void get(){
        for(int i = 0; i < p.size(); i++){
            if(p[i].get_c() >= 0){
                cout <<" + ";
            }
            cout << p[i].get_c() << "x";
            
            if(p[i].get_d() > 1){
                cout << "^" << p[i].get_d();
            }
                
        }
        cout << endl;
    }
    void operator=(Polynom const &a){
        p = a.p;
    }
    friend istream& operator>>(std::istream &in, Polynom &a);
    
    friend ostream& operator<<(std::ostream &out, Polynom &a);
    
    Polynom operator+(monomial a){
        set(a);
        return p;
    }
    Polynom operator+(Polynom &a){
        for(size_t i = 0; i < a.p.size(); i++)
            set(a.p[i]);
        return p;
    }
    
    Polynom operator-(monomial a){
        for(int i = 0; i < p.size(); i++){
            if(p[i].get_d() == a.get_d()){
                p[i].set_c(p[i].get_c() - a.get_c());
                continue;
            }
            else{
                a.set_c(a.get_c() * -1);
                p.push_back(a);
            }
        }
        return p;
    }
    Polynom operator-(Polynom &a){
        for(size_t i = 0; i < a.p.size(); i++){
            a.p[i].set_c(a.p[i].get_c() * -1);
            set(a.p[i]);
        }
        return p;
    }
    
    Polynom operator+=(monomial &a){
        operator+(a);
        return p;
    }
    Polynom operator+=(Polynom &a){
        operator+(a);
        return p;
    }
    
    Polynom operator-=(monomial &a){
        operator-(a);
        return p;
    }
    
    Polynom operator-=(Polynom &a){
        operator-(a);
        return p;
    }
    
    float operator[](int i){
        return p[i].get_c();
    }
    
    Polynom operator/(float const num){
        for(size_t i = 0; i < p.size(); i++){
            p[i].set_c(p[i].get_c() / num);
        }
        return p;
    }
    Polynom operator*(float const num){
        for(size_t i = 0; i < p.size(); i++){
            p[i].set_c(p[i].get_c() * num);
        }
        return p;
    }
    Polynom operator/=(float const num){
        for(size_t i = 0; i < p.size(); i++){
            p[i].set_c(p[i].get_c() / num);
        }
        return p;
    }
    Polynom operator*=(float const num){
        for(size_t i = 0; i < p.size(); i++){
            p[i].set_c(p[i].get_c() * num);
        }
        return p;
    }
    bool operator==(Polynom &a) {
        bool check = false;
        if(p.size() != a.p.size()){
            return false;
        }
        for(int i = 0; i < p.size(); i++){
            for(int j = 0; j < a.p.size(); j++){
                if(p[i].get_d() == a.p[j].get_d() && p[i].get_c() == a.p[j].get_c()){
                    check = true;
                }
                else check = false;
            }
        }
        return check;
    }
    
    bool operator!=(Polynom &a){
        return !operator==(a);
    }
    
    
    ~Polynom(){
      p.clear();
    }
    
};

ostream& operator<<(std::ostream &out, Polynom &a){
     for(int i = 0; i < a.p.size(); i++){
         if(a.p[i].get_c() >= 0){
             cout <<" + ";
         }
         cout << a.p[i].get_c() << "x";
         
         if(a.p[i].get_d() > 1){
             cout << "^" << a.p[i].get_d();
         }
     }
    return out;
 }

 istream& operator>>(std::istream &in, Polynom &a){
    float c;
    int degree;
    char sym;
    int n;
    cout << "введите кол - во одночленов";
    cin >> n;
    for(int i = 0; i < n; i++){
    cin  >> c >> sym >> degree;
    monomial o(c, degree);
    a.set(o);
    }
    return in;
 }

int main(int argc, const char * argv[]) {
    Polynom a;
    
    monomial b(5, 4);
    a.set(b);
    a.get();            // 5a^4
    
    monomial v(5, 4);
    monomial n(5, 1);
    
    a = a + v;         // 10a^4
    a.get();
    a = a + n;         // 10a^4 + 5
    a.get();
    
    Polynom m;
    m = a;
    if(a == m){
        cout << "работает блин капец жесть" << endl;
    }
    a = a + m;          // 20a^4 + 10
    a.get();
    
    monomial f(5, 1);
    m.set(f);
    
    a = a + m;          // 30a^4 + 15 + 5g
    a.get();
    
    
    a = a - m;
    
    a.get();
    
    if(a != m){
        cout << "работает блин капец жесть" << endl;
    }
    monomial k(10, 1);
    monomial s(20, 4);
    monomial z;
    
    Polynom q;
    q.set(k);
    q.set(s);
    if(m == q){
        cout << " m работает блин капец жесть" << endl;
    }
    Polynom e;
    e.set(k);
    cout << e << endl;
    e.get();
    Polynom i;
    cin >> i;
    cout << i;
    e = a / 2;
    return 0;
}

