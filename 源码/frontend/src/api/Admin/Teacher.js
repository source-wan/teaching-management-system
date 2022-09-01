import request from "@/utils/request";
//教师
export async function getTeacher(){
    return request.get(`/teacher`)
}


export async function postTeacher(model){
    return request.post(`/teacher`,model)
}


export async function delTeacher(teacherId){
    return request.delete(`/teacher/${teacherId}`)
}
