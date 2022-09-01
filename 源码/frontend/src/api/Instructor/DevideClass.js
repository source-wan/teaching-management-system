import request from "@/utils/request";

export async function getTeacherId(UserId){
    return request.get(`/teacher?UserId=${UserId}`)
}


export async function getClassList(InstroctorId){
    return request.get(`/class?instroctorId=${InstroctorId}`)
}



export async function getStudentList(majorId){
    return request.get(`/student?majorId=${majorId}`)
}


export async function putStudentList(Id,model){
    return request.put(`/student/${Id}`,model)
}


export async function allotStudent(classId,model){
    return request.post(`/classstudent/${classId}`,model)
}

export async function getClassStudentList(classId){
    return request.get(`/classstudent/${classId}`)
}


export async function getChangeClass(studentId){
    return request.get(`/classstudent/student/${studentId}`)
}



