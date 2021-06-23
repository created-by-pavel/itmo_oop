
#include <fstream>
#include <iostream>
#include <vector>
#include <ctime>
using namespace std;

class Frame {
public:
    char a, b, c, d, e, f, g, h, i;
    Frame(){};
    
    char operator[](char tmp){
        switch(tmp){
            case 'a':
                return a;
            case 'b':
                return b;
            case 'c':
                return c;
            case 'd':
                return d;
            case 'e':
                return e;
            case 'f':
                return f;
            case 'g':
                return g;
            case 'h':
                return h;
            case 'i':
                return i;
        }
        return 0;
    }

};


class Cube {
private:
    Frame f, b, r, l, u, d;
    
public:

    void str_out(string a) {
        bool v = false;
        if(v) {
            ofstream Fout("cubik.txt", ios_base::app);
            
            if(a == "close_file") {
                Fout.close();
            }
            
            if(Fout.is_open()) {
                Fout << a << endl;
            }
        }
    }
    
    void print(){
        cout << "       " << u.a << " " << u.b << " " << u.c << endl;
        cout << "       " << u.d << " " << u.e << " " << u.f << endl;
        cout << "       " << u.g << " " << u.h << " " << u.i << endl << endl;
        
        cout << l.a << " " << l.b << " " << l.c << "  " << f.a << " " << f.b << " " << f.c << "  ";
        cout << r.a << " " << r.b << " " << r.c << "  " << b.g << " " << b.h << " " << b.i << endl;
        cout << l.d << " " << l.e << " " << l.f << "  " << f.d << " " << f.e << " " << f.f << "  ";
        cout << r.d << " " << r.e << " " << r.f << "  " << b.d << " " << b.e << " " << b.f << endl;
        cout << l.g << " " << l.h << " " << l.i << "  " << f.g << " " << f.h << " " << f.i << "  ";
        cout << r.g << " " << r.h << " " << r.i << "  " << b.a << " " << b.b << " " << b.c << endl << endl;
        
        cout << "       " << d.a << " " << d.b << " " << d.c << endl;
        cout << "       " << d.d << " " << d.e << " " << d.f << endl;
        cout << "       " << d.g << " " << d.h << " " << d.i << endl;
        cout << endl;
        
    }
    
    void in() {
        
        ifstream fin("cube_in.txt");
        
        fin >> f.a >> f.b >> f.c >> f.d >> f.e >> f.f >> f.g >> f.h >> f.i;
        
        fin >> b.a >> b.b >> b.c >> b.d >> b.e >> b.f >> b.g >> b.h >> b.i;
        
        fin >> r.a >> r.b >> r.c >> r.d >> r.e >> r.f >> r.g >> r.h >> r.i;
        
        fin >> l.a >> l.b >> l.c >> l.d >> l.e >> l.f >> l.g >> l.h >> l.i;
        
        fin >> u.a >> u.b >> u.c >> u.d >> u.e >> u.f >> u.g >> u.h >> u.i;
        
        fin >> d.a >> d.b >> d.c >> d.d >> d.e >> d.f >> d.g >> d.h >> d.i;
        
        fin.close();
    }
    
    void out(string a){
        bool v = false;
        if(v){
        ofstream fout("Cube_out.txt", ios_base::app);
        fout << a << endl;
        
        fout << "       " << u.a << " " << u.b << " " << u.c << endl;
        fout << "       " << u.d << " " << u.e << " " << u.f << endl;
        fout << "       " << u.g << " " << u.h << " " << u.i << endl << endl;
        
        fout << l.a << " " << l.b << " " << l.c << "  " << f.a << " " << f.b << " " << f.c << "  ";
        fout << r.a << " " << r.b << " " << r.c << "  " << b.g << " " << b.h << " " << b.i << endl;
        fout << l.d << " " << l.e << " " << l.f << "  " << f.d << " " << f.e << " " << f.f << "  ";
        fout << r.d << " " << r.e << " " << r.f << "  " << b.d << " " << b.e << " " << b.f << endl;
        fout << l.g << " " << l.h << " " << l.i << "  " << f.g << " " << f.h << " " << f.i << "  ";
        fout << r.g << " " << r.h << " " << r.i << "  " << b.a << " " << b.b << " " << b.c << endl << endl;
        
        fout << "       " << d.a << " " << d.b << " " << d.c << endl;
        fout << "       " << d.d << " " << d.e << " " << d.f << endl;
        fout << "       " << d.g << " " << d.h << " " << d.i << endl;
        fout << endl;
        
        fout.close();
        }
    }
    
    void R_rotate(){
        
        char a_tmp = r.a;
        char b_tmp = r.b;
        char c_tmp = r.c;
        char d_tmp = r.d;
        char e_tmp = r.e;
        char f_tmp = r.f;
        char g_tmp = r.g;
        char h_tmp = r.h;
        char k_tmp = r.i;
        
        r.a = g_tmp;
        r.b = d_tmp;
        r.c = a_tmp;
        r.d = h_tmp;
        r.e = e_tmp;
        r.f = b_tmp;
        r.g = k_tmp;
        r.h = f_tmp;
        r.i = c_tmp;
        
        char tmp_1 = u.c;
        u.c = f.c;
        char tmp_2 = b.c;
        b.c = tmp_1;
        tmp_1 = d.c;
        d.c = tmp_2;
        f.c = tmp_1;
        
        tmp_1 = u.f;
        u.f = f.f;
        tmp_2 = b.f;
        b.f = tmp_1;
        tmp_1 = d.f;
        d.f = tmp_2;
        f.f = tmp_1;
        
        tmp_1 = u.i;
        u.i = f.i;
        tmp_2 = b.i;
        b.i = tmp_1;
        tmp_1 = d.i;
        d.i = tmp_2;
        f.i = tmp_1;
        str_out("R");
    }
    
    void R_rotate_inv() {
        char a_tmp = r.a;
        char b_tmp = r.b;
        char c_tmp = r.c;
        char d_tmp = r.d;
        char e_tmp = r.e;
        char f_tmp = r.f;
        char g_tmp = r.g;
        char h_tmp = r.h;
        char k_tmp = r.i;
        
        r.a = c_tmp;
        r.b = f_tmp;
        r.c = k_tmp;
        r.d = b_tmp;
        r.e = e_tmp;
        r.f = h_tmp;
        r.g = a_tmp;
        r.h = d_tmp;
        r.i = g_tmp;
        
        char tmp1 = u.c;
        u.c = b.c;
        char tmp2 = f.c;
        f.c = tmp1;
        tmp1 = d.c;
        d.c = tmp2;
        b.c = tmp1;
        
        tmp1 = u.f;
        u.f = b.f;
        tmp2 = f.f;
        f.f = tmp1;
        tmp1 = d.f;
        d.f = tmp2;
        b.f = tmp1;
        
        tmp1 = u.i;
        u.i = b.i;
        tmp2 = f.i;
        f.i = tmp1;
        tmp1 = d.i;
        d.i = tmp2;
        b.i = tmp1;
        str_out("R'");
    }
    
    void L_rotate(){
        
        char a_tmp = l.a;
        char b_tmp = l.b;
        char c_tmp = l.c;
        char d_tmp = l.d;
        char e_tmp = l.e;
        char f_tmp = l.f;
        char g_tmp = l.g;
        char h_tmp = l.h;
        char k_tmp = l.i;
        
        l.a = g_tmp;
        l.b = d_tmp;
        l.c = a_tmp;
        l.d = h_tmp;
        l.e = e_tmp;
        l.f = b_tmp;
        l.g = k_tmp;
        l.h = f_tmp;
        l.i = c_tmp;
        
        char tmp1 = u.a;
        u.a = b.a;
        char tmp2 = f.a;
        f.a = tmp1;
        tmp1 = d.a;
        d.a = tmp2;
        b.a = tmp1;
        
        tmp1 = u.d;
        u.d = b.d;
        tmp2 = f.d;
        f.d = tmp1;
        tmp1 = d.d;
        d.d = tmp2;
        b.d = tmp1;
        
        tmp1 = u.g;
        u.g = b.g;
        tmp2 = f.g;
        f.g = tmp1;
        tmp1 = d.g;
        d.g = tmp2;
        b.g = tmp1;
        str_out("L'");
        
    }
    
    void L_rotate_inv(){
        
        char a_tmp = l.a;
        char b_tmp = l.b;
        char c_tmp = l.c;
        char d_tmp = l.d;
        char e_tmp = l.e;
        char f_tmp = l.f;
        char g_tmp = l.g;
        char h_tmp = l.h;
        char k_tmp = l.i;
        
        l.a = c_tmp;
        l.b = f_tmp;
        l.c = k_tmp;
        l.d = b_tmp;
        l.e = e_tmp;
        l.f = h_tmp;
        l.g = a_tmp;
        l.h = d_tmp;
        l.i = g_tmp;
        
        char tmp1 = u.a;
        u.a = f.a;
        char tmp2 = b.a;
        b.a = tmp1;
        tmp1 = d.a;
        d.a = tmp2;
        f.a = tmp1;
        
        tmp1 = u.d;
        u.d = f.d;
        tmp2 = b.d;
        b.d = tmp1;
        tmp1 = d.d;
        d.d = tmp2;
        f.d = tmp1;
        
        tmp1 = u.g;
        u.g = f.g;
        tmp2 = b.g;
        b.g = tmp1;
        tmp1 = d.g;
        d.g = tmp2;
        f.g = tmp1;
        str_out("L'");
    }
    
    
    void F_rotate(){
        
        char a_tmp = f.a;
        char b_tmp = f.b;
        char c_tmp = f.c;
        char d_tmp = f.d;
        char e_tmp = f.e;
        char f_tmp = f.f;
        char g_tmp = f.g;
        char h_tmp = f.h;
        char k_tmp = f.i;
        
        f.a = g_tmp;
        f.b = d_tmp;
        f.c = a_tmp;
        f.d = h_tmp;
        f.e = e_tmp;
        f.f = b_tmp;
        f.g = k_tmp;
        f.h = f_tmp;
        f.i = c_tmp;
        
        char tmp1 = r.g;
        r.g = u.i;
        char tmp2 = d.a;
        d.a = tmp1;
        tmp1 = l.c;
        l.c = tmp2;
        u.i = tmp1;
        
        tmp1 = r.d;
        r.d = u.h;
        tmp2 = d.b;
        d.b = tmp1;
        tmp1 = l.f;
        l.f = tmp2;
        u.h = tmp1;
        
        tmp1 = r.a;
        r.a = u.g;
        tmp2 = d.c;
        d.c = tmp1;
        tmp1 = l.i;
        l.i = tmp2;
        u.g = tmp1;
        
        str_out("F");
    }
    
    void F_rotate_inv(){
        
        char a_tmp = f.a;
        char b_tmp = f.b;
        char c_tmp = f.c;
        char d_tmp = f.d;
        char e_tmp = f.e;
        char f_tmp = f.f;
        char g_tmp = f.g;
        char h_tmp = f.h;
        char k_tmp = f.i;
        
        f.a = c_tmp;
        f.b = f_tmp;
        f.c = k_tmp;
        f.d = b_tmp;
        f.e = e_tmp;
        f.f = h_tmp;
        f.g = a_tmp;
        f.h = d_tmp;
        f.i = g_tmp;
        
        char tmp1 = u.i;
        u.i = r.g;
        char tmp2 = l.c;
        l.c = tmp1;
        tmp1 = d.a;
        d.a = tmp2;
        r.g = tmp1;
        
        tmp1 = u.h;
        u.h = r.d;
        tmp2 = l.f;
        l.f = tmp1;
        tmp1 = d.b;
        d.b = tmp2;
        r.d = tmp1;
        
        tmp1 = u.g;
        u.g = r.a;
        tmp2 = l.i;
        l.i = tmp1;
        tmp1 = d.c;
        d.c = tmp2;
        r.a = tmp1;
        str_out("F'");
    }
    
    void B_rotate(){
        
        char a_tmp = b.a;
        char b_tmp = b.b;
        char c_tmp = b.c;
        char d_tmp = b.d;
        char e_tmp = b.e;
        char f_tmp = b.f;
        char g_tmp = b.g;
        char h_tmp = b.h;
        char k_tmp = b.i;
        
        b.a = g_tmp;
        b.b = d_tmp;
        b.c = a_tmp;
        b.d = h_tmp;
        b.e = e_tmp;
        b.f = b_tmp;
        b.g = k_tmp;
        b.h = f_tmp;
        b.i = c_tmp;
        
        char tmp1 = l.a;
        l.a = u.c;
        char tmp2 = d.g;
        d.g = tmp1;
        tmp1 = r.i;
        r.i = tmp2;
        u.c = tmp1;
        
        tmp1 = l.d;
        l.d = u.b;
        tmp2 = d.h;
        d.h = tmp1;
        tmp1 = r.f;
        r.f = tmp2;
        u.b = tmp1;
        
        tmp1 = l.g;
        l.g = u.a;
        tmp2 = d.i;
        d.i = tmp1;
        tmp1 = r.c;
        r.c = tmp2;
        u.a = tmp1;
        str_out("B");
    }
    
    void B_rotate_inv(){
        
        char a_tmp = b.a;
        char b_tmp = b.b;
        char c_tmp = b.c;
        char d_tmp = b.d;
        char e_tmp = b.e;
        char f_tmp = b.f;
        char g_tmp = b.g;
        char h_tmp = b.h;
        char k_tmp = b.i;
        
        b.a = c_tmp;
        b.b = f_tmp;
        b.c = k_tmp;
        b.d = b_tmp;
        b.e = e_tmp;
        b.f = h_tmp;
        b.g = a_tmp;
        b.h = d_tmp;
        b.i = g_tmp;
        
        char tmp1 = r.i;
        r.i = u.c;
        char tmp2 = d.g;
        d.g = tmp1;
        tmp1 = l.a;
        l.a = tmp2;
        u.c = tmp1;
        
        tmp1 = r.f;
        r.f = u.b;
        tmp2 = d.h;
        d.h = tmp1;
        tmp1 = l.d;
        l.d = tmp2;
        u.b = tmp1;
        
        tmp1 = r.c;
        r.c = u.a;
        tmp2 = d.i;
        d.i = tmp1;
        tmp1 = l.g;
        l.g = tmp2;
        u.a = tmp1;
        str_out("B'");
    }
    
    void U_rotate(){
        
        char a_tmp = u.a;
        char b_tmp = u.b;
        char c_tmp = u.c;
        char d_tmp = u.d;
        char e_tmp = u.e;
        char f_tmp = u.f;
        char g_tmp = u.g;
        char h_tmp = u.h;
        char k_tmp = u.i;
        
        u.a = g_tmp;
        u.b = d_tmp;
        u.c = a_tmp;
        u.d = h_tmp;
        u.e = e_tmp;
        u.f = b_tmp;
        u.g = k_tmp;
        u.h = f_tmp;
        u.i = c_tmp;
        
        char tmp1 = f.a;
        f.a = r.a;
        char tmp2 = l.a;
        l.a = tmp1;
        tmp1 = b.i;
        b.i = tmp2;
        r.a = tmp1;
        
        tmp1 = f.b;
        f.b = r.b;
        tmp2 = l.b;
        l.b = tmp1;
        tmp1 = b.h;
        b.h = tmp2;
        r.b = tmp1;
        
        tmp1 = f.c;
        f.c = r.c;
        tmp2 = l.c;
        l.c = tmp1;
        tmp1 = b.g;
        b.g = tmp2;
        r.c = tmp1;
        str_out("U");
    }
    
    void U_rotate_inv(){
        
        char a_tmp = u.a;
        char b_tmp = u.b;
        char c_tmp = u.c;
        char d_tmp = u.d;
        char e_tmp = u.e;
        char f_tmp = u.f;
        char g_tmp = u.g;
        char h_tmp = u.h;
        char k_tmp = u.i;
        
        u.a = c_tmp;
        u.b = f_tmp;
        u.c = k_tmp;
        u.d = b_tmp;
        u.e = e_tmp;
        u.f = h_tmp;
        u.g = a_tmp;
        u.h = d_tmp;
        u.i = g_tmp;
        
        char tmp1 = r.a;
        r.a = f.a;
        char tmp2 = b.i;
        b.i = tmp1;
        tmp1 = l.a;
        l.a = tmp2;
        f.a = tmp1;
        
        tmp1 = r.b;
        r.b = f.b;
        tmp2 = b.h;
        b.h = tmp1;
        tmp1 = l.b;
        l.b = tmp2;
        f.b = tmp1;
        
        tmp1 = r.c;
        r.c = f.c;
        tmp2 = b.g;
        b.g = tmp1;
        tmp1 = l.c;
        l.c = tmp2;
        f.c = tmp1;
        str_out("U'");
    }
    
    void D_rotate(){
        
        char a_tmp = d.a;
        char b_tmp = d.b;
        char c_tmp = d.c;
        char d_tmp = d.d;
        char e_tmp = d.e;
        char f_tmp = d.f;
        char g_tmp = d.g;
        char h_tmp = d.h;
        char k_tmp = d.i;
        
        d.a = c_tmp;
        d.b = f_tmp;
        d.c = k_tmp;
        d.d = b_tmp;
        d.e = e_tmp;
        d.f = h_tmp;
        d.g = a_tmp;
        d.h = d_tmp;
        d.i = g_tmp;
        
        char tmp1 = f.g;
        f.g = r.g;
        char tmp2 = l.g;
        l.g = tmp1;
        tmp1 = b.c;
        b.c = tmp2;
        r.g = tmp1;
        
        tmp1 = f.h;
        f.h = r.h;
        tmp2 = l.h;
        l.h = tmp1;
        tmp1 = b.b;
        b.b = tmp2;
        r.h = tmp1;
        
        tmp1 = f.i;
        f.i = r.i;
        tmp2 = l.i;
        l.i = tmp1;
        tmp1 = b.a;
        b.a = tmp2;
        r.i = tmp1;
        str_out("D");
    }
    
    void D_rotate_inv(){
        
        char a_tmp = d.a;
        char b_tmp = d.b;
        char c_tmp = d.c;
        char d_tmp = d.d;
        char e_tmp = d.e;
        char f_tmp = d.f;
        char g_tmp = d.g;
        char h_tmp = d.h;
        char k_tmp = d.i;
        
        d.a = c_tmp;
        d.b = f_tmp;
        d.c = k_tmp;
        d.d = b_tmp;
        d.e = e_tmp;
        d.f = h_tmp;
        d.g = a_tmp;
        d.h = d_tmp;
        d.i = g_tmp;
        
        char tmp1 = r.g;
        r.g = f.g;
        char tmp2 = b.c;
        b.c = tmp1;
        tmp1 = l.g;
        l.g = tmp2;
        f.g = tmp1;
        
        tmp1 = r.h;
        r.h = f.h;
        tmp2 = b.b;
        b.b = tmp1;
        tmp1 = l.h;
        l.h = tmp2;
        f.h = tmp1;
        
        tmp1 = r.i;
        r.i = f.i;
        tmp2 = b.a;
        b.a = tmp1;
        tmp1 = l.i;
        l.i = tmp2;
        f.i = tmp1;
        str_out("D'");
    }
    
    void randomization(){
        
        int swaps = abs(rand() % 10);
        //srand(time(0));
        for (int i = 0; i < swaps; ++i) {
            int x = abs(rand() % 11);
            switch(x){
                case 0:
                    R_rotate();
                    break;
                case 1:
                    L_rotate();
                    break;
                case 2:
                    R_rotate_inv();
                    break;
                case 3:
                    F_rotate();
                    break;
                case 4:
                    L_rotate_inv();
                    break;
                case 5:
                    F_rotate_inv();
                    break;
                case 6:
                    B_rotate_inv();
                    break;
                case 7:
                    U_rotate();
                    break;
                case 8:
                    B_rotate();
                    break;
                case 9:
                    U_rotate_inv();
                    break;
                case 10:
                    D_rotate();
                    break;
            }
        }
        cout << endl;
        str_out("end of randomization");
    }
    
    
    bool check_cube(){
        
        vector<char> v_check[6];                       // (0,y), (1,g), (2,w), (3,r), (4,o), (5,b)
        for(char j = 'a'; j <= 'i'; j++){
            char tmp = f[j];
            
            switch (tmp){
                case 'y':
                    v_check[0].push_back(tmp);
                    break;
                case 'g':
                    v_check[1].push_back(tmp);
                    break;
                case 'w':
                    v_check[2].push_back(tmp);
                    break;
                case 'r':
                    v_check[3].push_back(tmp);
                    break;
                case 'o':
                    v_check[4].push_back(tmp);
                    break;
                case 'b':
                    v_check[5].push_back(tmp);
                    break;
            }
        }
        for(char j = 'a'; j <= 'i'; j++){
            char tmp = u[j];
    
            switch (tmp){
                case 'y':
                    v_check[0].push_back(tmp);
                    break;
                case 'g':
                    v_check[1].push_back(tmp);
                    break;
                case 'w':
                    v_check[2].push_back(tmp);
                    break;
                case 'r':
                    v_check[3].push_back(tmp);
                    break;
                case 'o':
                    v_check[4].push_back(tmp);
                    break;
                case 'b':
                    v_check[5].push_back(tmp);
                    break;
            }
        }
        for(char j = 'a'; j <= 'i'; j++){
            char tmp = d[j];
            
            switch (tmp){
                case 'y':
                    v_check[0].push_back(tmp);
                    break;
                case 'g':
                    v_check[1].push_back(tmp);
                    break;
                case 'w':
                    v_check[2].push_back(tmp);
                    break;
                case 'r':
                    v_check[3].push_back(tmp);
                    break;
                case 'o':
                    v_check[4].push_back(tmp);
                    break;
                case 'b':
                    v_check[5].push_back(tmp);
                    break;
            }
        }
        for(char j = 'a'; j <= 'i'; j++){
            char tmp = r[j];
            
            switch (tmp){
                case 'y':
                    v_check[0].push_back(tmp);
                    break;
                case 'g':
                    v_check[1].push_back(tmp);
                    break;
                case 'w':
                    v_check[2].push_back(tmp);
                    break;
                case 'r':
                    v_check[3].push_back(tmp);
                    break;
                case 'o':
                    v_check[4].push_back(tmp);
                    break;
                case 'b':
                    v_check[5].push_back(tmp);
                    break;
            }
        }
        for(char j = 'a'; j <= 'i'; j++){
            char tmp = l[j];
            
            switch (tmp){
                case 'y':
                    v_check[0].push_back(tmp);
                    break;
                case 'g':
                    v_check[1].push_back(tmp);
                    break;
                case 'w':
                    v_check[2].push_back(tmp);
                    break;
                case 'r':
                    v_check[3].push_back(tmp);
                    break;
                case 'o':
                    v_check[4].push_back(tmp);
                    break;
                case 'b':
                    v_check[5].push_back(tmp);
                    break;
            }
        }
        for(char j = 'a'; j <= 'i'; j++){
            char tmp = b[j];
            
            switch (tmp){
                case 'y':
                    v_check[0].push_back(tmp);
                    break;
                case 'g':
                    v_check[1].push_back(tmp);
                    break;
                case 'w':
                    v_check[2].push_back(tmp);
                    break;
                case 'r':
                    v_check[3].push_back(tmp);
                    break;
                case 'o':
                    v_check[4].push_back(tmp);
                    break;
                case 'b':
                    v_check[5].push_back(tmp);
                    break;
            }
        }
        
        for(int i = 0; i < 6; i++){
            if(v_check[i].size() != 9){
                cout << "что-то пошло не так и кубик is dead inside (умер)" << endl;
                return false;
            }
        }
        //cout << "correct" << endl;
        return true;
    }
    
    bool check_solve(){
        return f.a == 'g' && f.b == 'g' && f.c == 'g' && f.d == 'g' && f.e == 'g' && f.f == 'g' && f.g == 'g' && f.h == 'g' && f.i == 'g' &&
        b.a == 'b' && b.b == 'b' && b.c == 'b' && b.d == 'b' && b.e == 'b' && b.f == 'b' && b.g == 'b' && b.h == 'b' && b.i == 'b' &&
        r.a == 'o' && r.b == 'o' && r.c == 'o' && r.d == 'o' && r.e == 'o' && r.f == 'o' && r.g == 'o' && r.h == 'o' && r.i == 'o' &&
        l.a == 'r' && l.b == 'r' && l.c == 'r' && l.d == 'r' && l.e == 'r' && l.f == 'r' && l.g == 'r' && l.h == 'r' && l.i == 'r' &&
        u.a == 'y' && u.b == 'y' && u.c == 'y' && u.d == 'y' && u.e == 'y' && u.f == 'y' && u.g == 'y' && u.h == 'y' && u.i == 'y' &&
        d.a == 'w' && d.b == 'w' && d.c == 'w' && d.d == 'w' && d.e == 'w' && d.f == 'w' && d.g == 'w' && d.h == 'w' && d.i == 'w';
    }
    
    bool check_all_corners(){
        return ((u.i == 'y' && f.c == 'g' && r.a == 'o') || (u.i == 'y' && f.c == 'o' && r.a == 'g') ||
                (u.i == 'g' && f.c == 'y' && r.a == 'o') || (u.i == 'g' && f.c == 'o' && r.a == 'y') ||
                (u.i == 'o' && f.c == 'g' && r.a == 'y') || (u.i == 'o' && f.c == 'y' && r.a == 'g')) &&
                ((f.a == 'g' && l.c == 'r' && u.g == 'y') || (f.a == 'g' && l.c == 'y' && u.g == 'r') ||
                 (f.a == 'r' && l.c == 'y' && u.g == 'g') || (f.a == 'r' && l.c == 'g' && u.g == 'y') ||
                (f.a == 'y' && l.c == 'r' && u.g == 'g') || (f.a == 'y' && l.c == 'g' && u.g == 'r')) &&
                ((u.c == 'y' && r.c == 'o' && b.i == 'b') || (u.c == 'y' && r.c == 'b' && b.i == 'o') ||
                (u.c == 'o' && r.c == 'y' && b.i == 'b') || (u.c == 'o' && r.c == 'b' && b.i == 'y') ||
                (u.c == 'b' && r.c == 'o' && b.i == 'y') || (u.c == 'b' && r.c == 'y' && b.i == 'o')) &&
                ((u.a == 'y' && b.g == 'b' && l.a == 'r') || (u.a == 'y' && b.g == 'r' && l.a == 'b') ||
                (u.a == 'r' && b.g == 'b' && l.a == 'y') || (u.a == 'r' && b.g == 'y' && l.a == 'b') ||
                (u.a == 'b' && b.g == 'y' && l.a == 'r') || (u.a == 'b' && b.g == 'r' && l.a == 'y'));
    }
    
    bool check_one_corner(){
        return ((u.i == 'y' && f.c == 'g' && r.a == 'o') || (u.i == 'y' && f.c == 'o' && r.a == 'g') ||
                (u.i == 'g' && f.c == 'y' && r.a == 'o') || (u.i == 'g' && f.c == 'o' && r.a == 'y') ||
                (u.i == 'o' && f.c == 'g' && r.a == 'y') || (u.i == 'o' && f.c == 'y' && r.a == 'g')) ||
                ((f.a == 'g' && l.c == 'r' && u.g == 'y') || (f.a == 'g' && l.c == 'y' && u.g == 'r') ||
                (f.a == 'r' && l.c == 'y' && u.g == 'g') || (f.a == 'r' && l.c == 'g' && u.g == 'y') ||
                 (f.a == 'y' && l.c == 'r' && u.g == 'g') || (f.a == 'y' && l.c == 'g' && u.g == 'r')) ||
                ((u.c == 'y' && r.c == 'o' && b.i == 'b') || (u.c == 'y' && r.c == 'b' && b.i == 'o') ||
                 (u.c == 'o' && r.c == 'y' && b.i == 'b') || (u.c == 'o' && r.c == 'b' && b.i == 'y') ||
                 (u.c == 'b' && r.c == 'o' && b.i == 'y') || (u.c == 'b' && r.c == 'y' && b.i == 'o')) ||
                ((u.a == 'y' && b.g == 'b' && l.a == 'r') || (u.a == 'y' && b.g == 'r' && l.a == 'b') ||
                 (u.a == 'r' && b.g == 'b' && l.a == 'y') || (u.a == 'r' && b.g == 'y' && l.a == 'b') ||
                 (u.a == 'b' && b.g == 'y' && l.a == 'r') || (u.a == 'b' && b.g == 'r' && l.a == 'y'));
    }
    
    void solve_cube(){
        
        if (!check_solve() && check_cube()) {
            
            while (u.b != 'w' || u.d != 'w' || u.f != 'w' || u.h != 'w') {      /// пока цветок не собран
                if (f.f == 'w') {
                    while (u.f == 'w') {
                        U_rotate();
                    }
                    R_rotate();
                }
                if (f.d == 'w') {
                    while (u.d == 'w') {
                        U_rotate();
                    }
                    L_rotate_inv();
                }
                if (r.d == 'w') {
                    while (u.h == 'w') {
                        U_rotate();
                    }
                    F_rotate_inv();
                }
                if (r.f == 'w') {
                    while (u.b == 'w') {
                        U_rotate();
                    }
                    B_rotate();
                }
                if (l.f == 'w') {
                    while (u.h == 'w') {
                        U_rotate();
                    }
                    F_rotate();
                }
                if (l.d == 'w') {
                    while (u.b == 'w') {
                        U_rotate();
                    }
                    B_rotate_inv();
                }
                if (b.f == 'w') {
                    while (u.f == 'w') {
                        U_rotate();
                    }
                    R_rotate_inv();
                }
                if (b.d == 'w') {
                    while (u.d == 'w') {
                        U_rotate();
                    }
                    L_rotate();
                }
                if (d.f == 'w') {
                    while (u.f == 'w') {
                        U_rotate();
                    }
                    R_rotate();
                    R_rotate();
                }
                if (d.h == 'w') {
                    while (u.b == 'w') {
                        U_rotate();
                    }
                    B_rotate_inv();
                    B_rotate_inv();
                }
                if (d.d == 'w') {
                    while (u.d == 'w') {
                        U_rotate();
                    }
                    L_rotate_inv();
                    L_rotate_inv();
                }
                if (d.b == 'w') {
                    while (u.h == 'w') {
                        U_rotate();
                    }
                    F_rotate();
                    F_rotate();
                }
                
                if (f.h == 'w') {
                    F_rotate();
                    while (u.d == 'w') {
                        U_rotate();
                    }
                    L_rotate_inv();
                }
                if (f.b == 'w') {
                    F_rotate();
                    while (u.f == 'w') {
                        U_rotate();
                    }
                    R_rotate();
                }
                if (r.h == 'w') {
                    R_rotate();
                    while (u.h == 'w') {
                        U_rotate();
                    }
                    F_rotate_inv();
                }
                if (r.b == 'w') {
                    R_rotate();
                    while (u.b == 'w') {
                        U_rotate();
                    }
                    B_rotate();
                }
                if (b.b == 'w') {
                    B_rotate_inv();
                    while (u.d == 'w') {
                        U_rotate();
                    }
                    L_rotate();
                }
                if (b.h == 'w') {
                    B_rotate_inv();
                    while (u.f == 'w') {
                        U_rotate();
                    }
                    R_rotate_inv();
                }
                if (l.h == 'w') {
                    L_rotate_inv();
                    while (u.h == 'w') {
                        U_rotate();
                    }
                    F_rotate();
                }
                if (l.b == 'w') {
                    L_rotate_inv();
                    while (u.b == 'w') {
                        U_rotate();
                    }
                    B_rotate_inv();
                }
            }
            
            str_out("собран цветок");
            
            while (f.b != f.e || u.h != 'w') {
                U_rotate();
            }
            F_rotate();
            F_rotate();
            while (r.b != r.e || u.f != 'w') {
                U_rotate();
            }
            R_rotate();
            R_rotate();
            while (b.h != b.e || u.b != 'w') {
                U_rotate();
            }
            B_rotate();
            B_rotate();
            while (l.b != l.e || u.d != 'w') {
                U_rotate();
            }
            L_rotate();
            L_rotate();
            
            str_out ("белый крест ");
            out("белый крест");
            
            int count = 0;
            while (d.a != 'w' || d.c != 'w' || d.g != 'w' || d.i != 'w') {          /// пока не собран 1 слой
                
                if (b.i == 'w') {
                    if ((u.c == 'o' && r.c == 'b') || (u.c == 'b' && r.c == 'o')) {
                        while (d.i != 'w' || r.i != 'o' || b.c != 'b') {
                            B_rotate();
                            U_rotate();
                            B_rotate_inv();
                            U_rotate_inv();
                        }
                        
                    }
                    if ((u.c == 'o' && r.c == 'g' && b.i == 'w') || (u.c == 'g' && r.c == 'o' && b.i == 'w')) {
                        U_rotate();
                        while (d.c != 'w' || f.i != 'g' || r.g != 'o') {
                            R_rotate();
                            U_rotate();
                            R_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.c == 'g' && r.c == 'r' && b.i == 'w') || (u.c == 'r' && r.c == 'g' && b.i == 'w')) {
                        U_rotate();
                        U_rotate();
                        while (d.a != 'w' || l.i != 'r' || f.g != 'g') {
                            F_rotate();
                            U_rotate();
                            F_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.c == 'r' && r.c == 'b' && b.i == 'w') || (u.c == 'b' && r.c == 'r' && b.i == 'w')) {
                        U_rotate_inv();
                        while (d.g != 'w' || b.a != 'b' || l.g != 'r') {
                            L_rotate();
                            U_rotate();
                            L_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                }
                
                if (u.c == 'w') {
                    if ((b.i == 'o' && r.c == 'b') || (b.i == 'b' && r.c == 'o')) {
                        while (d.i != 'w' || r.i != 'o' || b.c != 'b') {
                            B_rotate();
                            U_rotate();
                            B_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((b.i == 'o' && r.c == 'g' && u.c == 'w') || (b.i == 'g' && r.c == 'o' && u.c == 'w')) {
                        U_rotate();
                        while (d.c != 'w' || f.i != 'g' || r.g != 'o') {
                            R_rotate();
                            U_rotate();
                            R_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((b.i == 'g' && r.c == 'r' && u.c == 'w') || (b.i == 'r' && r.c == 'g' && u.c == 'w')) {
                        U_rotate();
                        U_rotate();
                        while (d.a != 'w' || l.i != 'r' || f.g != 'g') {
                            F_rotate();
                            U_rotate();
                            F_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((b.i == 'r' && r.c == 'b' && u.c == 'w') || (b.i == 'b' && r.c == 'r' && u.c == 'w')) {
                        U_rotate_inv();
                        while (d.g != 'w' || b.a != 'b' || l.g != 'r') {
                            L_rotate();
                            U_rotate();
                            L_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                }
                
                if (r.c == 'w') {
                    if ((u.c == 'o' && b.i == 'b') || (u.c == 'b' && b.i == 'o')) {
                        while (d.i != 'w' || r.i != 'o' || b.c != 'b') {
                            B_rotate();
                            U_rotate();
                            B_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.c == 'o' && b.i == 'g' && r.c == 'w') || (u.c == 'g' && b.i == 'o' && r.c == 'w')) {
                        U_rotate();
                        while (d.c != 'w' || f.i != 'g' || r.g != 'o') {
                            R_rotate();
                            U_rotate();
                            R_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.c == 'g' && b.i == 'r' && r.c == 'w') || (u.c == 'r' && b.i == 'g' && r.c == 'w')) {
                        U_rotate();
                        U_rotate();
                        while (d.a != 'w' || l.i != 'r' || f.g != 'g') {
                            F_rotate();
                            U_rotate();
                            F_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.c == 'r' && b.i == 'b' && r.c == 'w') || (u.c == 'b' && b.i == 'r' && r.c == 'w')) {
                        U_rotate_inv();
                        while (d.g != 'w' || b.a != 'b' || l.g != 'r') {
                            L_rotate();
                            U_rotate();
                            L_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                }
                if (b.g == 'w') {
                    if ((u.a == 'o' && l.a == 'b') || (u.a == 'b' && l.a == 'o')) {
                        U_rotate();
                        while (d.i != 'w' || r.i != 'o' || b.c != 'b') {
                            B_rotate();
                            U_rotate();
                            B_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.a == 'o' && l.a == 'g' && b.g == 'w') || (u.a == 'g' && l.a == 'o' && b.g == 'w')) {
                        U_rotate();
                        U_rotate();
                        while (d.c != 'w' || f.i != 'o' || r.g != 'o') {
                            R_rotate();
                            U_rotate();
                            R_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.a == 'g' && l.a == 'r' && b.g == 'w') || (u.a == 'r' && l.a == 'g' && b.g == 'w')) {
                        U_rotate_inv();
                        while (d.a != 'w' || l.i != 'r' || f.g != 'g') {
                            F_rotate();
                            U_rotate();
                            F_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.a == 'r' && l.a == 'b' && b.g == 'w') || (u.a == 'b' && l.a == 'r' && b.g == 'w')) {
                        while (d.g != 'w' || b.a != 'b' || l.g != 'r') {
                            L_rotate();
                            U_rotate();
                            L_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                }
                
                if (u.a == 'w') {
                    if ((b.g == 'o' && l.a == 'b') || (b.g == 'b' && l.a == 'o')) {
                        U_rotate();
                        while (d.i != 'w' || r.i != 'o' || b.c != 'b') {
                            B_rotate();
                            U_rotate();
                            B_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((b.g == 'o' && l.a == 'g' && u.a == 'w') || (b.g == 'g' && l.a == 'o' && u.a == 'w')) {
                        U_rotate();
                        U_rotate();
                        while (d.c != 'w' || f.i != 'g' || r.g != 'o') {
                            R_rotate();
                            U_rotate();
                            R_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((b.g == 'g' && l.a == 'r' && u.a == 'w') || (b.g == 'r' && l.a == 'g' && u.a == 'w')) {
                        U_rotate_inv();
                        while (d.a != 'w' || l.i != 'r' || f.g != 'g') {
                            F_rotate();
                            U_rotate();
                            F_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((b.g == 'r' && l.a == 'b' && u.a == 'w') || (b.g == 'b' && l.a == 'r' && u.a == 'w')) {
                        while (d.g != 'w' || b.a != 'b' || l.g != 'r') {
                            L_rotate();
                            U_rotate();
                            L_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                }
                
                if (l.a == 'w') {
                    if ((u.a == 'o' && b.g == 'b') || (u.a == 'b' && b.g == 'o')) {
                        U_rotate();
                        while (d.i != 'w' || r.i != 'o' || b.c != 'b') {
                            B_rotate();
                            U_rotate();
                            B_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.a == 'o' && b.g == 'g' && l.a == 'w') || (u.a == 'g' && b.g == 'o' && l.a == 'w')) {
                        U_rotate();
                        U_rotate();
                        while (d.c != 'w' || f.i != 'g' || r.g != 'o') {
                            R_rotate();
                            U_rotate();
                            R_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.a == 'g' && b.g == 'r' && l.a == 'w') || (u.a == 'r' && b.g == 'g' && l.a == 'w')) {
                        U_rotate_inv();
                        while (d.a != 'w' || l.i != 'r' || f.g != 'g') {
                            F_rotate();
                            U_rotate();
                            F_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.a == 'r' && b.g == 'b' && l.a == 'w') || (u.a == 'b' && b.g == 'r' && l.a == 'w')) {
                        while (d.g != 'w' || b.a != 'b' || l.g != 'r') {
                            L_rotate();
                            U_rotate();
                            L_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                } /// 2 угол
                
                if (f.a == 'w') {
                    if ((u.g == 'o' && l.c == 'b') || (u.g == 'b' && l.c == 'o')) {
                        U_rotate();
                        U_rotate();
                        while (d.i != 'w' || r.i != 'o' || b.c != 'b') {
                            B_rotate();
                            U_rotate();
                            B_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.g == 'o' && l.c == 'g' && f.a == 'w') || (u.g == 'g' && l.c == 'o' && f.a == 'w')) {
                        U_rotate_inv();
                        while (d.c != 'w' || f.i != 'g' || r.g != 'o') {
                            R_rotate();
                            U_rotate();
                            R_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.g == 'g' && l.c == 'r' && f.a == 'w') || (u.g == 'r' && l.c == 'g' && f.a == 'w')) {
                        while (d.a != 'w' || l.i != 'r' || f.g != 'g') {
                            F_rotate();
                            U_rotate();
                            F_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.g == 'r' && l.c == 'b' && f.a == 'w') || (u.g == 'b' && l.c == 'r' && f.a == 'w')) {
                        U_rotate();
                        while (d.g != 'w' || b.a != 'b' || l.g != 'r') {
                            L_rotate();
                            U_rotate();
                            L_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                }
                
                if (u.g == 'w') {
                    if ((f.a == 'o' && l.c == 'b') || (f.a == 'b' && l.c == 'o')) {
                        U_rotate();
                        U_rotate();
                        while (d.i != 'w' || r.i != 'o' || b.c != 'b') {
                            B_rotate();
                            U_rotate();
                            B_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((f.a == 'o' && l.c == 'g' && u.g == 'w') || (f.a == 'g' && l.c == 'o' && u.g == 'w')) {
                        U_rotate_inv();
                        while (d.c != 'w' || f.i != 'g' || r.g != 'o') {
                            R_rotate();
                            U_rotate();
                            R_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((f.a == 'g' && l.c == 'r' && u.g == 'w') || (f.a == 'r' && l.c == 'g' && u.g == 'w')) {
                        while (d.a != 'w' || l.i != 'r' || f.g != 'g') {
                            F_rotate();
                            U_rotate();
                            F_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((f.a == 'r' && l.c == 'b' && u.g == 'w') || (f.a == 'b' && l.c == 'r' && u.g == 'w')) {
                        U_rotate();
                        while (d.g != 'w' || b.a != 'b' || l.g != 'r') {
                            L_rotate();
                            U_rotate();
                            L_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                }
                
                if (l.c == 'w') {
                    if ((u.g == 'o' && f.a == 'b') || (u.g == 'b' && f.a == 'o')) {
                        U_rotate();
                        U_rotate();
                        while (d.i != 'w' || r.i != 'o' || b.c != 'b') {
                            B_rotate();
                            U_rotate();
                            B_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.g == 'o' && f.a == 'g' && l.c == 'w') || (u.g == 'g' && f.a == 'o' && l.c == 'w')) {
                        U_rotate_inv();
                        while (d.c != 'w' || f.i != 'g' || r.g != 'o') {
                            R_rotate();
                            U_rotate();
                            R_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.g == 'g' && f.a == 'r' && l.c == 'w') || (u.g == 'r' && f.a == 'g' && l.c == 'w')) {
                        while (d.a != 'w' || l.i != 'r' || f.g != 'g') {
                            F_rotate();
                            U_rotate();
                            F_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.g == 'r' && f.a == 'b' && l.c == 'w') || (u.g == 'b' && f.a == 'r' && l.c == 'w')) {
                        U_rotate();
                        while (d.g != 'w' || b.a != 'b' || l.g != 'r') {
                            L_rotate();
                            U_rotate();
                            L_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                } /// 3 угол
                
                if (f.c == 'w') {
                    if ((u.i == 'o' && r.a == 'b') || (u.i == 'b' && r.a == 'o')) {
                        U_rotate_inv();
                        while (d.i != 'w' || r.i != 'o' || b.c != 'b') {
                            B_rotate();
                            U_rotate();
                            B_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.i == 'o' && r.a == 'g' && f.c == 'w') || (u.i == 'g' && r.a == 'o' && f.c == 'w')) {
                        while (d.c != 'w' || f.i != 'g' || r.g != 'o') {
                            R_rotate();
                            U_rotate();
                            R_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.i == 'g' && r.a == 'r' && f.c == 'w') || (u.i == 'r' && r.a == 'g' && f.c == 'w')) {
                        U_rotate();
                        while (d.a != 'w' || l.i != 'r' || f.g != 'g') {
                            F_rotate();
                            U_rotate();
                            F_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((u.i == 'r' && r.a == 'b' && f.c == 'w') || (u.i == 'b' && r.a == 'r' && f.c == 'w')) {
                        U_rotate();
                        U_rotate();
                        while (d.g != 'w' || b.a != 'b' || l.g != 'r') {
                            L_rotate();
                            U_rotate();
                            L_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                }
                
                if (u.i == 'w') {
                    if ((f.c == 'o' && r.a == 'b') || (f.c == 'b' && r.a == 'o')) {
                        U_rotate_inv();
                        while (d.i != 'w' || r.i != 'o' || b.c != 'b') {
                            B_rotate();
                            U_rotate();
                            B_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((f.c == 'o' && r.a == 'g' && u.i == 'w') || (f.c == 'g' && r.a == 'o' && u.i == 'w')) {
                        while (d.c != 'w' || f.i != 'g' || r.g != 'o') {
                            R_rotate();
                            U_rotate();
                            R_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((f.c == 'g' && r.a == 'r' && u.i == 'w') || (f.c == 'r' && r.a == 'g' && u.i == 'w')) {
                        U_rotate();
                        while (d.a != 'w' || l.i != 'r' || f.g != 'g') {
                            F_rotate();
                            U_rotate();
                            F_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((f.c == 'r' && r.a == 'b' && u.i == 'w') || (f.c == 'b' && r.a == 'r' && u.i == 'w')) {
                        U_rotate();
                        U_rotate();
                        while (d.g != 'w' || b.a != 'b' || l.g != 'r') {
                            L_rotate();
                            U_rotate();
                            L_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                }
                
                if (r.a == 'w') {
                    if ((f.c == 'o' && u.i == 'b' && r.a == 'w') || (f.c == 'b' && u.i == 'o' && r.a == 'w')) {
                        U_rotate_inv();
                        while (d.i != 'w' || r.i != 'o' || b.c != 'b') {
                            B_rotate();
                            U_rotate();
                            B_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((f.c == 'o' && u.i == 'g' && r.a == 'w') || (f.c == 'g' && u.i == 'o' && r.a == 'w')) {
                        while (d.c != 'w' || f.i != 'g' || r.g != 'o') {
                            R_rotate();
                            U_rotate();
                            R_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((f.c == 'g' && u.i == 'r' && r.a == 'w') || (f.c == 'r' && u.i == 'g' && r.a == 'w')) {
                        U_rotate();
                        while (d.a != 'w' || l.i != 'r' || f.g != 'g') {
                            F_rotate();
                            U_rotate();
                            F_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                    if ((f.c == 'r' && u.i == 'b' && r.a == 'w') || (f.c == 'b' && u.i == 'r' && r.a == 'w')) {
                        U_rotate();
                        U_rotate();
                        while (d.g != 'w' || b.a != 'b' || l.g != 'r') {
                            L_rotate();
                            U_rotate();
                            L_rotate_inv();
                            U_rotate_inv();
                        }
                    }
                }
                
                if ((f.i == 'w' && r.g == 'o' && d.c == 'g') || (f.i == 'w' && r.g == 'g' && d.c == 'o') ||
                    (f.i == 'o' && r.g == 'w' && d.c == 'g') || (f.i == 'g' && r.g == 'w' && d.c == 'o') ||
                    (f.i == 'o' && r.g == 'g' && d.c == 'w')) {
                    while (d.c != 'w' || f.i != 'g' || r.g != 'o') {
                        R_rotate();
                        U_rotate();
                        R_rotate_inv();
                        U_rotate_inv();
                    }
                }
                if ((b.c == 'w' && r.i == 'o' && d.i == 'b') || (b.c == 'w' && r.i == 'b' && d.i == 'o') ||
                    (b.c == 'o' && r.i == 'w' && d.i == 'b') || (b.c == 'b' && r.i == 'w' && d.i == 'o') ||
                    (b.c == 'o' && r.i == 'b' && d.i == 'w')) {
                    while (d.i != 'w' || b.c != 'b' || r.i != 'o') {
                        B_rotate();
                        U_rotate();
                        B_rotate_inv();
                        U_rotate_inv();
                    }
                }
                if ((b.a == 'w' && l.g == 'r' && d.g == 'b') || (b.a == 'w' && l.g == 'b' && d.g == 'r') ||
                    (b.a == 'r' && l.g == 'w' && d.g == 'b') || (b.a == 'b' && l.g == 'w' && d.g == 'r') ||
                    (b.a == 'r' && l.g == 'b' && d.g == 'w')) {
                    while (d.g != 'w' || b.a != 'b' || l.g != 'r') {
                        L_rotate();
                        U_rotate();
                        L_rotate_inv();
                        U_rotate_inv();
                    }
                }
                if ((l.i == 'w' && f.g == 'g' && d.a == 'r') || (l.i == 'w' && f.g == 'r' && d.a == 'g') ||
                    (l.i == 'g' && f.g == 'w' && d.a == 'r') || (l.i == 'r' && f.g == 'w' && d.a == 'g') ||
                    (l.i == 'g' && f.g == 'r' && d.a == 'w')) {
                    while (d.a != 'w' || l.i != 'r' || f.g != 'g') {
                        F_rotate();
                        U_rotate();
                        F_rotate_inv();
                        U_rotate_inv();
                    }
                }
                
                if ((d.c == 'w' && (f.i != 'g' && f.i != 'o')) || (d.c == 'w' && (r.g != 'g' && r.g != 'o'))) {
                    R_rotate();
                    U_rotate();
                    R_rotate_inv();
                    U_rotate_inv();
                }
                if ((d.i == 'w' && (r.i != 'b' && r.i != 'o')) || (d.i == 'w' && (b.c != 'b' && b.c != 'o'))) {
                    B_rotate();
                    U_rotate();
                    B_rotate_inv();
                    U_rotate_inv();
                }
                if ((d.g == 'w' && (b.a != 'b' && b.a != 'r')) || (d.g == 'w' && (l.g != 'b' && l.g != 'r'))) {
                    L_rotate();
                    U_rotate();
                    L_rotate_inv();
                    U_rotate_inv();
                }
                if ((d.a == 'w' && (f.g != 'g' && f.g != 'r')) || (d.a == 'w' && (l.i != 'g' && l.i != 'r'))) {
                    F_rotate();
                    U_rotate();
                    F_rotate_inv();
                    U_rotate_inv();
                }
            }
            str_out("нижний слой построен");
            out("нижний слой построен");
            int pred = -1;
            while (f.d != 'g' || f.f != 'g' || r.d != 'o' || r.f != 'o' || b.f != 'b' || b.d != 'b' || l.d != 'r' || l.f != 'r') {
                count = 0;                  /// пока 2 слой  не будет построен
                pred = -1;
                while (count > pred) {
                    pred = count;
                    if (f.b == 'g' || r.b == 'g' || b.h == 'g' || l.b == 'g') {
                        while (f.b != 'g') {                /// совмещаем центры
                            U_rotate();
                        }
                        if (u.h == 'o') {                   /// если g-o
                            count++;
                            U_rotate();
                            R_rotate();
                            U_rotate();
                            R_rotate_inv();
                            U_rotate_inv();
                            F_rotate_inv();
                            U_rotate_inv();
                            F_rotate();
                            U_rotate();
                        } else {
                            if (u.h == 'r') {                   /// если g-r
                                count++;
                                U_rotate_inv();
                                L_rotate_inv();
                                U_rotate_inv();
                                L_rotate();
                                U_rotate();
                                F_rotate();
                                U_rotate();
                                F_rotate_inv();
                                U_rotate_inv();                     /// g-y и g-b  не рассматриваем
                            }
                        }
                    }
                    if (f.b == 'o' || r.b == 'o' || b.h == 'o' || l.b == 'o') {
                        while (r.b != 'o') {
                            U_rotate();
                        }
                        if (u.f == 'b') {
                            count++;
                            U_rotate();
                            B_rotate();
                            U_rotate();
                            B_rotate_inv();
                            U_rotate_inv();
                            R_rotate_inv();
                            U_rotate_inv();
                            R_rotate();
                            U_rotate();
                        } else {
                            if (u.f == 'g') {
                                count++;
                                U_rotate_inv();
                                F_rotate_inv();
                                U_rotate_inv();
                                F_rotate();
                                U_rotate();
                                R_rotate();
                                U_rotate();
                                R_rotate_inv();
                                U_rotate_inv();
                            }
                        }
                    }
                    if (f.b == 'b' || r.b == 'b' || b.h == 'b' || l.b == 'b') {
                        while (b.h != 'b') {
                            U_rotate();
                        }
                        if (u.b == 'r') {
                            count++;
                            U_rotate();
                            L_rotate();
                            U_rotate();
                            L_rotate_inv();
                            U_rotate_inv();
                            B_rotate_inv();
                            U_rotate_inv();
                            B_rotate();
                            U_rotate();
                        } else {
                            if (u.b == 'o') {
                                count++;
                                U_rotate_inv();
                                R_rotate_inv();
                                U_rotate_inv();
                                R_rotate();
                                U_rotate();
                                B_rotate();
                                U_rotate();
                                B_rotate_inv();
                                U_rotate_inv();
                            }
                        }
                    }
                    if (f.b == 'r' || r.b == 'r' || b.h == 'r' || l.b == 'r') {
                        while (l.b != 'r') {
                            U_rotate();
                        }
                        if (u.d == 'g') {
                            count++;
                            U_rotate();
                            F_rotate();
                            U_rotate();
                            F_rotate_inv();
                            U_rotate_inv();
                            L_rotate_inv();
                            U_rotate_inv();
                            L_rotate();
                            U_rotate();
                        } else {
                            if (u.d == 'b') {
                                count++;
                                U_rotate_inv();
                                B_rotate_inv();
                                U_rotate_inv();
                                B_rotate();
                                U_rotate();
                                L_rotate();
                                U_rotate();
                                L_rotate_inv();
                                U_rotate_inv();
                            }
                        }
                    }
                }                                   /// если  'y' ставим на вверх
                if (f.f != 'g') {
                    R_rotate();
                    U_rotate();
                    R_rotate_inv();
                    U_rotate_inv();
                    F_rotate_inv();
                    U_rotate_inv();
                    F_rotate();
                    U_rotate();
                }
                if (r.f != 'o') {
                    B_rotate();
                    U_rotate();
                    B_rotate_inv();
                    U_rotate_inv();
                    R_rotate_inv();
                    U_rotate_inv();
                    R_rotate();
                    U_rotate();
                }
                if (b.d != 'b') {
                    L_rotate();
                    U_rotate();
                    L_rotate_inv();
                    U_rotate_inv();
                    B_rotate_inv();
                    U_rotate_inv();
                    B_rotate();
                    U_rotate();
                }
                if (l.f != 'r') {
                    F_rotate();
                    U_rotate();
                    F_rotate_inv();
                    U_rotate_inv();
                    L_rotate_inv();
                    U_rotate_inv();
                    L_rotate();
                    U_rotate();
                }
            } ///Собраны нижний и средний  слои
            
            str_out("среднйи слой и нижний слой собраны");
            out("средний слой и нижний слой собраны");
        
            while (u.d != 'y' || u.b != 'y' || u.f != 'y' || u.h != 'y') {              /// пока не крест
                if ((u.f == 'y' && u.b == 'y') || (u.b == 'y' && u.h == 'y')) {
                    L_rotate();
                    F_rotate();
                    U_rotate();
                    F_rotate_inv();
                    U_rotate_inv();
                    L_rotate_inv();
                } else {
                    if (u.f == 'y' && u.h == 'y') {         /// стрелки часов
                        B_rotate();
                        L_rotate();
                        U_rotate();
                        L_rotate_inv();
                        U_rotate_inv();
                        B_rotate_inv();
                    } else {
                        if (u.d == 'y' && u.h == 'y') {
                            R_rotate();
                            B_rotate();
                            U_rotate();
                            B_rotate_inv();
                            U_rotate_inv();
                            R_rotate_inv();
                        } else {
                            F_rotate();                 /// горизонтально
                            R_rotate();
                            U_rotate();
                            R_rotate_inv();
                            U_rotate_inv();
                            F_rotate_inv();
                        }
                    }
                }
            } ///Желтый крест
            str_out("желтый крест собран");
            
            int check = 0;                                                      /// делаем правильный крест
            if (f.b == 'g' && r.b == 'o' && b.h == 'b' && l.b == 'r') {         ///  если повезло
                check = 1;
            } else {
                U_rotate();
                if (f.b == 'g' && r.b == 'o' && b.h == 'b' && l.b == 'r') {
                    check = 1;
                } else {
                    U_rotate();
                    if (f.b == 'g' && r.b == 'o' && b.h == 'b' && l.b == 'r') {
                        check = 1;
                    } else {
                        U_rotate();
                        if (f.b == 'g' && r.b == 'o' && b.h == 'b' && l.b == 'r') {
                            check = 1;
                        }
                    }
                }
            }
            if (check == 0) {                                                       /// если не повезло
                while (f.b != 'g' || r.b != 'o' || b.h != 'b' || l.b != 'r') {
                    U_rotate();
                    if (r.b == 'o' && b.h == 'b') {     /// если соседние
                        R_rotate();
                        U_rotate();
                        R_rotate_inv();
                        U_rotate();
                        R_rotate();
                        U_rotate();
                        U_rotate();
                        R_rotate_inv();
                        U_rotate();
                        break;
                    }
                    if (r.b == 'o' && f.b == 'g') {
                        F_rotate();
                        U_rotate();
                        F_rotate_inv();
                        U_rotate();
                        F_rotate();
                        U_rotate();
                        U_rotate();
                        F_rotate_inv();
                        U_rotate();
                        break;
                    }
                    if (f.b == 'g' && l.b == 'r') {
                        L_rotate();
                        U_rotate();
                        L_rotate_inv();
                        U_rotate();
                        L_rotate();
                        U_rotate();
                        U_rotate();
                        L_rotate_inv();
                        U_rotate();
                        break;
                    }
                    if (l.b == 'r' && b.h == 'b') {
                        B_rotate();
                        U_rotate();
                        B_rotate_inv();
                        U_rotate();
                        B_rotate();
                        U_rotate();
                        U_rotate();
                        B_rotate_inv();
                        U_rotate();
                        break;
                    }
                    if (r.b == 'o' && l.b == 'r') {         /// если напротив
                        B_rotate();
                        U_rotate();
                        B_rotate_inv();
                        U_rotate();
                        B_rotate();
                        U_rotate();
                        U_rotate();
                        B_rotate_inv();
                    }
                    if (f.b == 'g' && b.h == 'b') {             //если напротив
                        R_rotate();
                        U_rotate();
                        R_rotate_inv();
                        U_rotate();
                        R_rotate();
                        U_rotate();
                        U_rotate();
                        R_rotate_inv();
                    }
                }
            }                                           /// праавильный крест
            
            
            while (!check_all_corners()) {                                       /// совпадение с центарми у 1 угла
                if ((u.i == 'y' && f.c == 'g' && r.a == 'o') || (u.i == 'y' && f.c == 'o' && r.a == 'g') ||
                    (u.i == 'g' && f.c == 'y' && r.a == 'o') || (u.i == 'g' && f.c == 'o' && r.a == 'y') ||
                    (u.i == 'o' && f.c == 'g' && r.a == 'y') || (u.i == 'o' && f.c == 'y' && r.a == 'g')) {
                    while (!check_all_corners()) {
                        U_rotate();
                        R_rotate();
                        U_rotate_inv();
                        L_rotate_inv();
                        U_rotate();
                        R_rotate_inv();
                        U_rotate_inv();
                        L_rotate();
                    }
                } else {
                    if ((f.a == 'g' && l.c == 'r' && u.g == 'y') || (f.a == 'g' && l.c == 'y' && u.g == 'r') ||
                        (f.a == 'r' && l.c == 'y' && u.g == 'g') || (f.a == 'r' && l.c == 'g' && u.g == 'y') ||
                        (f.a == 'y' && l.c == 'r' && u.g == 'g') || (f.a == 'y' && l.c == 'g' && u.g == 'r')) {
                        while (!check_all_corners()) {
                            U_rotate();
                            F_rotate();
                            U_rotate_inv();
                            B_rotate_inv();
                            U_rotate();
                            F_rotate_inv();
                            U_rotate_inv();
                            B_rotate();
                        }
                    } else {
                        if ((u.c == 'y' && r.c == 'o' && b.i == 'b') || (u.c == 'y' && r.c == 'b' && b.i == 'o') ||
                            (u.c == 'o' && r.c == 'y' && b.i == 'b') || (u.c == 'o' && r.c == 'b' && b.i == 'y') ||
                            (u.c == 'b' && r.c == 'o' && b.i == 'y') || (u.c == 'b' && r.c == 'y' && b.i == 'o')) {
                            while (!check_all_corners()) {
                                U_rotate();
                                B_rotate();
                                U_rotate_inv();
                                F_rotate_inv();
                                U_rotate();
                                B_rotate_inv();
                                U_rotate_inv();
                                F_rotate();
                            }
                        } else {
                            if ((u.a == 'y' && b.g == 'b' && l.a == 'r') || (u.a == 'y' && b.g == 'r' && l.a == 'b') ||
                                (u.a == 'r' && b.g == 'b' && l.a == 'y') || (u.a == 'r' && b.g == 'y' && l.a == 'b') ||
                                (u.a == 'b' && b.g == 'y' && l.a == 'r') || (u.a == 'b' && b.g == 'r' && l.a == 'y')) {
                                while (!check_all_corners()) {
                                    U_rotate();
                                    L_rotate();
                                    U_rotate_inv();
                                    R_rotate_inv();
                                    U_rotate();
                                    L_rotate_inv();
                                    U_rotate_inv();
                                    R_rotate();
                                }
                            } else {
                                while (!check_one_corner()) {           /// если вообще нет углов после появится 1
                                    U_rotate();
                                    R_rotate();
                                    U_rotate_inv();
                                    L_rotate_inv();
                                    U_rotate();
                                    R_rotate_inv();
                                    U_rotate_inv();
                                    L_rotate();
                                }
                            }
                        }
                    }
                }
            } /// поставили углы
            
            if (u.c != 'y') {
                while (u.c != 'y') {
                    B_rotate();
                    R_rotate_inv();         /// пиф - паф
                    B_rotate_inv();
                    R_rotate();
                }
            }
            U_rotate();
            if (u.c != 'y') {
                while (u.c != 'y') {
                    B_rotate();
                    R_rotate_inv();
                    B_rotate_inv();
                    R_rotate();
                }
            }
            U_rotate();
            if (u.c != 'y') {
                while (u.c != 'y') {
                    B_rotate();
                    R_rotate_inv();
                    B_rotate_inv();
                    R_rotate();
                }
            }
            U_rotate();
            if (u.c != 'y') {
                while (u.c != 'y') {
                    B_rotate();
                    R_rotate_inv();
                    B_rotate_inv();
                    R_rotate();
                }
            }
            while (!check_solve()) {
                U_rotate();
            }
        }
        str_out("вверхний слой собран!");
    }
};

int main(){
    Cube c;
    
    c.in();
    c.print();
    
    cout << endl <<  "кручу верчу запутать хочу" << endl;
    c.randomization();
    //c.check_cube();
    c.print();
    
    cout << endl <<  "решено" << endl;
    c.solve_cube();
    c.print();
    c.out("собран");
    c.str_out("close_file");
    return 0;
    
}

