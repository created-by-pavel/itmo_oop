//
//  main.cpp
//  4(шаблоны)
//
//  Created by pavel on 20.03.2021.
//

#include <iostream>
#include <vector>
using namespace std;

template <typename T>
bool comp(T first, T second) {
    if(second < first) return false;
    else return true;
};

template <typename T>
bool N_B_T_5(T x) {
    return x > 5;
};


template<typename T, typename func>
bool all_off(T begin, const T end, func predicat){
    while(begin != end){
        if(!predicat(*begin)) return false;
        begin++;
    }
    return true;

}

template<typename T, typename func>
bool any_off(T begin, const T end, func predicat){
    while(begin != end){
        if(predicat(*begin)) return true;
        begin++;
    }
    return false;
}

template<typename T, typename func>
bool none_off(T begin, const T &end, func predicat){
    while(begin != end){
        if(predicat(*begin)) return false;
        begin++;
    }
    return true;
}

template<typename T, typename func>
bool one_off(const T &begin, const T &end, func predicat){
    short count = 0;
    while(begin != end){
        if(predicat(*begin)) count++;
        begin++;
    }
    if(count == 1){
        return true;
    }
    else return false;
}

template <typename T, typename func>
bool iss_sorted (T begin, const T& end, func predicat) {
    while(begin != end){
        if(!predicat(*begin, *(begin + 1))) return false;
        begin++;
    }
    return true;
}

template<typename T, typename E>
E find_not(T begin, const T &end, E el){
    while(begin != end){
        if(*begin != el) return *begin;
        begin++;
    }
    return NULL;        
}

template<typename T, typename E>
E find_backward(const T &begin, T end, E el){
    while(end != begin){
        if(*end == el) return *end;
        end--;
    }
    return NULL;
}

template<typename T, typename func>
bool is_palindrome(T begin, T end, func predicat){
    while(begin <= end){
        if(*begin != *end) return false;
        begin++;
        end--;
    }
    return true;
}

template <typename T, typename func>
bool is__partitioned (T begin, T end, func predicat) {
    while(begin != end){
        if (!predicat(*begin))
            break;
        begin++;
    }
    while(begin != end){
        if (predicat(*begin))
            return false;
        begin++;
    }
    return true;
}





int main(int argc, const char * argv[]) {
    
    vector<short> v = {3, 4, 5, 6, 7, 8, 9};
    
    cout << all_off(v.begin(), v.end(), N_B_T_5<int>) << endl;
    cout << any_off(v.begin(), v.end(), N_B_T_5<int>) << endl;
    cout << none_off(v.begin(), v.end(), N_B_T_5<int>) << endl;
    cout << none_off(v.begin(), v.end(), N_B_T_5<int>) << endl;
    cout << iss_sorted(v.begin(), v.end(), comp<int>) << endl;
    cout << is__partitioned(v.begin(), v.end(), N_B_T_5<int>) << endl;
    cout << find_not(v.begin(), v.end(), 1) << endl;
    cout << find_backward(v.begin(), v.end(), 8) << endl;
    cout << is_palindrome(v.begin(), v.end(), N_B_T_5<int>) << endl;
    

    return 0;
}

