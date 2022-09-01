import request from "@/utils/request";

export async function getClassList(MajorId){
    return request.get(`/class?MajorId=${MajorId}`)
}

export async function getClassCourseList(ClassId){
    return request.get(`/classcourse?ClassId=${ClassId}`)
}

export async function getCourseDetail(courseId){
    return request.get(`/course/${courseId}`)
}

export async function getCourseDataList(){
    return request.get(`/course`)
}


export async function getTeacherDataList(){
    return request.get(`/teacher`)
}



export async function getBookDataList(){
    return request.get(`/textbook`)
}


export async function postClassCourse(model){
    return request.post(`/classcourse`,model)
}

export async function postCourseTeacher(model){
    return request.post(`/courseteacher`,model)
}

export async function postCourseTextBook(model){
    return request.post(`/coursetextbook`,model)
}

export async function delClassCourse(classCourseId){
    return request.delete(`/classcourse/${classCourseId}`)
}