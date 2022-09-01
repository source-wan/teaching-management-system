import request from "@/utils/request";

export async function getTeacher(userId){
    return request.get(`/teacher?userId=${userId}`)
}

export async function getClassCourse(teacherId){
    return request.get(`/classcourse?teacherId=${teacherId}`)
}

export async function getCourse(courseId){
    return request.get(`/course?id=${courseId}`)
}

export async function getClass(classId){
    return request.get(`/class?id=${classId}`)
}
export async function getClassstudent(classId){
    return request.get(`/classstudent/${classId}`)
}
export async function postScore(classId,model){
    return request.post(`/score/${classId}`,model)
}
