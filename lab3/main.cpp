//
//  main.cpp
//  анализДанных
//
//  Created by pavel on 24.03.2021.
//
/*Маршрут с наибольшим количеством остановок по отдельными видам
 транспорта*/

#include <iostream>
#include "pugixml.cpp"
#include <string>
#include <map>
using namespace pugi;

class Routes{
private:
    int max = -1;
    std::string max_name = "";
    std::map <std::string, int> routes;
    std::string vechicle;
    
public:
    Routes(const std::string vechicle_):vechicle(vechicle_){}
    
    std::string get_MN(){
        if(max_name != "") return max_name;
        return "i dont know";
    }
    
    int get_M(){
        return max;
    }
    
    void Get(){
        xml_document doc;                   // создаем класс
        if(!doc.load_file("data.xml")){
            std:: cout << "oops smth was wrong";
            exit(1);
        }
        xml_node node = doc.first_child();        // спускаемся на dataset
        for (xml_node it = node.first_child(); it; it = it.next_sibling()) {
            std::string transport;
            for (xml_node it2 = it.first_child(); it2; it2 = it2.next_sibling()) {
                std::string tag = it2.name();
                std::string info = it2.child_value();      // идем по тегам каждый раз в value присваиваем инфо тэга
                
                if(tag == "type_of_vehicle")    transport = info;
                
                if((transport == vechicle) && (tag == "routes")){
                    
                    std::string tmp;
                    for(char i : info){
            
                        if(i == ',' || i == '.'){
                            routes[tmp]++;
                            tmp = "";
                        }
                        else tmp = tmp + i;
                    }
                    if(tmp != "" && tmp != " "){            // после точки или запятой
                        routes[tmp]++;
                    }
                }
            }
        }
    }
    
    void max_route(){
        for(std::map<std::string, int>::iterator it = routes.begin(); it != routes.end(); it++){
            if(it -> second > max){
                max_name = it -> first;
                max = it -> second;
            }
        }
        std::cout << "Route-" << max_name << " - " << max << std::endl;
    }
    
};


int main(){
    
    Routes route("Трамвай");
    route.Get();
    route.max_route();
    
    return 0;
}
