import request from "@/utils/request";


export async function findStudent(UserId){
    return request.get(`/student?UserId=${UserId}`)
}

export async function putStudent(studentId,model){
    return request.put(`/student/${studentId}`,model)
}

export async function getScore(){
    return request.get(`/score`)
}