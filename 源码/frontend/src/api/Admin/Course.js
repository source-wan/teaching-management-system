import request from "@/utils/request";

export async function getCourse(input){
    return request.get(`/course?courseName=${input}`)
}


export async function putCourse(courseId,model){
    return request.put(`/course/${courseId}`,model)
}


export async function postCourse(model){
    return request.post(`/course`,model)
}
export async function delCourse(courseId){
    return request.delete(`/course/${courseId}`)
}