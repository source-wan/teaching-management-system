let routes = [
    {
        path: "/",
        component: () => import('../components/LayoutBase'),
        meta: {
            title: '首页',
            hidden: true,
        },
        redirect: '/BaseCharts',
        children: [
            {
                path: 'BaseCharts',
                component: () => import('../views/HomePage/BaseCharts'),
                meta: {
                    icon: 'el-icon-s-home',
                    title: '后台管理系统',
                    hidden: true
                },
            }
        ]
    },
    {
        path: "/Admin",
        meta: {
            title: '管理员端',
            icon: 'el-icon-user'
        },
        component: () => import('../components/LayoutBase'),
        children: [
            {
                path: "CollegeSet",
                meta: {
                    title: '学院信息',
                    icon: '',
                    hidden: false
                },
                component: () => import('../views/Admin/CollegeSet')
            },   
            {
                path: "CollegeManage",
                meta: {
                    title: '学院设置',
                    icon: '',
                    hidden: false
                },
                component: () => import('../views/Admin/CollegeManage')
            },
            {
                path: "TeacherSet",
                meta: {
                    title: '教师设置',
                    icon: '',
                    hidden: false
                },
                component: () => import('../views/Admin/TeacherSet')
            },
            {
                path: "BookSet",
                meta: {
                    title: '教材设置',
                    icon: '',
                    hidden: false
                },
                component: () => import('../views/Admin/BookSet')
            },
            {
                path: "CourseSet",
                meta: {
                    title: '课程设置',
                    icon: '',
                    hidden: false
                },
                component: () => import('../views/Admin/CourseSet')
            },
            {
                path: "CourseManage",
                meta: {
                    title: '课程安排',
                    icon: '',
                    hidden: false
                },
                component: () => import('../views/Admin/CourseManage')
            },
        ]
    },
    {
        path: "/Instructor",
        meta: {
            title: '辅导员端',
            icon: 'el-icon-user'
        },
        component: () => import('../components/LayoutBase'),
        children: [
            {
                path: "DevideClass",
                meta: {
                    title: '班级管理',
                    icon: '',
                    hidden: false
                },
                component: () => import('../views/Instructor/DevideClass')
            },
            {
                path: "DevideDorm",
                meta: {
                    title: '宿舍管理',
                    icon: '',
                    hidden: false
                },
                component: () => import('../views/Instructor/DevideDorm')
            },
           
        ]
    },
     {
        path: "/Teacher",
        meta: {
            title: '教师端',
            icon: 'el-icon-user',
            hidden: false,
        },
        component: () => import('../components/LayoutBase'),
        children: [
            {
                path: "ScoreInsert",
                meta: {
                    title: '成绩录入',
                    icon: '',
                    hidden: false
                },
                component: () => import('../views/Teacher/ScoreInsert')
            },
            {
                path: "SatisfactionSurvey",
                meta: {
                    title: '满意度调查',
                    icon: '',
                    hidden: false
                },
                component: () => import('../views/Teacher/SatisfactionSurvey')
            },
            {
                path:'/CheckSurvey',
                meta:{
                    hidden:true
                },
                component:()=>import('../views/Survey/CheckSurvey.vue')
            },
            {
                path:'/CheckSurveyData',
                meta:{
                    hidden:true
                },
                component:()=>import('../views/Survey/CheckSurveyData.vue')
            },
            // {
            //     path: "CoursePlan",
            //     meta: {
            //         title: '排课',
            //         icon: '',
            //         hidden: false
            //     },
            //     component: () => import('../views/Teacher/CoursePlan')
            // },

            // {
            //     path: "test",
            //     meta: {
            //         hidden: false
            //     },
            //     component: () => import('../views/Teacher/TestTotable')
            // }
        ]
    },
    {
        path: "/Student",
        meta: {
            title: '学生端',
            icon: 'el-icon-user'
        },
        component: () => import('../components/LayoutBase'),
        children: [
            {
                path: "StudentInfo",
                meta: {
                    title: '学生卡片',
                    icon: '',
                    hidden: false
                },
                component: () => import('../views/Student/InformationQuery/StudentCord')
            },
            // {
            //     path: "MyCourse",
            //     meta: {
            //         title: '课程表',
            //         icon: '',
            //         hidden: false
            //     },
            //     component: () => import('../views/Student/InformationQuery/MyCourse')
            // },
            {
                path: "MyScore",
                meta: {
                    title: '成绩查询',
                    icon: '',
                    hidden: false
                },
                component: () => import('../views/Student/InformationQuery/MyScore')
            },
            // {
            //     path: "OptionalCourse",
            //     meta: {
            //         title: '选课',
            //         icon: '',
            //         hidden: false
            //     },
            //     component: () => import('../views/Student/OptionalCourse/SelectCourse')
            // }
        ]
    },
    {
        path: '/Login',
        meta: {
            hidden: true
        },
        component: () => import('../views/Login/LoginView')
    },
    {
        path: '*',
        meta: {
            hidden: true
        },
        component: () => import('../components/NotFound')
    },
]

export default routes;