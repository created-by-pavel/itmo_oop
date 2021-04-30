//
//  main.cpp
//  кольцевой буфер
//
//  Created by pavel on 12.04.2021.
//

#include <iostream>
#include<iterator>
#include <algorithm>
using namespace std;

template <class T>
class Circular_buffer{
private:
    int head = 0;
    int tail = 0;
    int capacity = 0;
    int size = 0;
    T* buffer = nullptr;
public:
    class Iterator : public iterator<random_access_iterator_tag, T>{
    private:
        T* point = nullptr;
    public:
        Iterator() : point(nullptr){}
        Iterator(T *p) : point(p){}
        
        Iterator& operator++(){
            ++point;
            return *this;
        }
        
        Iterator operator++ (T){
            Iterator tmp(*this);
            operator++();
            return tmp;
        }
        
        T& operator *(){
            return *point;
        }
        
        Iterator& operator--(){
            point--;
            return *this;
        }
        Iterator operator-(T value){
            return Iterator(point - value);
        }
        Iterator operator+(T value) {
            return Iterator(point + value);
        }
        
        T& operator[] (T value){
            return point[value];
        }
        
        T* operator ->(){
            return point;
        }
        Iterator& operator+=(T value) {
            point += value;
            return *this;
        }
        Iterator& operator-=(T value) {
            point -= value;
            return *this;
        }
        bool operator == (Iterator& value){
            return value.point == this->point;
        }
       bool operator != (Iterator& value){
            return value.point = point;
        }
        bool operator < (Iterator& value){
            return value.point < point;
        }
        bool operator > (Iterator& value){
            return value.point > this->point;
        }
        bool operator >= (Iterator& value){
            return value.point >= this->point;
        }
        
        bool operator <= (Iterator value){
            return this->point <= value.point;
        }
        
    };
    Circular_buffer<T>(int capacity_){      // выделили память
        capacity = capacity_;
        buffer = new T[capacity];
    }
    void push_back(T value){
        if(tail == capacity){
            buffer[capacity - 1] = value;
        }
        else{
            buffer[tail] = value;
            tail++;
        }
    }
    void pop_back(){
        buffer[tail] = NULL;
        tail--;
    }
    void push_front(T value){
        if(tail == capacity){
            buffer[head] = buffer[tail];
        }
        else{
            for(int i = tail; i >= head; i--){
            buffer[i + 1] = buffer[i];
            }
            buffer[head] = value;
            tail++;
        }
        
    }
    void print(){
        for(Iterator i = begin(); i <= end() - 1; ++i){
            cout << *i << " ";
        }
        cout << endl;
    }
    
    void pop_fornt(){
        for(int i = head; i <= tail; i++){
            buffer[i] = buffer[i + 1];
        }
        buffer[tail] = NULL;
        tail--;
    }
    
    Iterator begin() const{
        return Iterator(buffer + head);
    }
    
    Iterator end() const{
        return Iterator(buffer + tail);
    }
    
    T get(int index){
        return buffer[index];
    }
    
    void new_capacity(int new_c){
        T* new_buffer = new T[capacity];
        for(int i = head;i < tail; ++i){
            new_buffer[i] = buffer[i];
        }
        capacity = new_c;
        buffer = new T[new_c];
        for(int i = head; i < tail; i++){
            buffer[i] = new_buffer[i];
        }
    }
    void print_c(){
        cout << capacity << " " << endl;
        
        auto found = find(begin(), end(), (int)2);
        cout << *found << endl;
        
        //sort(begin(), end());
       // auto Max = max(begin(), end());
    }
};


int main(int argc, const char * argv[]) {
    Circular_buffer<int> B(10);
    B.push_back(3);
    B.push_back(2);
    B.push_back(0);
    B.push_front(5);
    B.pop_fornt();
    B.pop_back();
    B.print();
    cout << B.get(0) << endl;
    B.new_capacity(20);
    B.print_c();
    
    return 0;
}

