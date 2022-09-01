<template>
  <view v-if="isHide">
    <view v-if="canIUse">
      <view class="header">
        <image
          src="https://thirdwx.qlogo.cn/mmopen/vi_32/POgEwh4mIHO4nibH0KlMECNjjGxQUq24ZEaGT4poC6icRiccVGKSyXwibcPq4BWmiaIGuG1icwxaQX6grC9VemZoJ8rg/132"
        >
        </image>
      </view>

      <view class="content">
        <view>申请获取以下权限</view>
        <text>获得你的公开信息(昵称，头像等)</text>
      </view>
      <button
        class="bottom"
        type="primary"
        open-type="getUserInfo"
        lang="zh_CN"
        @click="bindGetUserInfo"
      >
        授权登录
      </button>
    </view>
    <view v-else>请升级微信版本</view>
  </view>

  <view v-else>
    <view>
      <uni-section type="line" padding>
        <view class="studentnum">
          <text class="uni-subtitle">学号</text>
        </view>
        <view class="studentnumtwo">
          <uni-easyinput
            class="uni-mt-5"
            trim="all"
            v-model="value"
            placeholder="请输入学号"
          >
          </uni-easyinput>
        </view>
      </uni-section>
    </view>
    <view>
      <button @click="binding">绑定</button>
    </view>
  </view>
</template>

<script>
const app = getApp();
export default {
  data() {
    return {
      value: "",
      canIUse: uni.canIUse("button.open-type.getUserInfo"),
      // 记得改true
      isHide: true,
      date: {
        AvatarUrl: "",
        UserName: "",
        WeCharId: "",
        Sid: "",
      },
    };
  },
  onLoad: function () {
    var that = this;
    // 查看是否授权
    uni.getSetting({
      success: function (res) {
        if (res.authSetting["scope.userInfo"]) {
          uni.getUserInfo({
            desc: "完善信息",
            success: function (res) {
              uni.login({
                success: (res) => {
                  uni.request({
                    url:
                      "https://api.weixin.qq.com/sns/jscode2session?appid=wxfe85c92a33a8fb07&secret=820f54f6b3547215a59cd4511be30fe3&js_code=" +
                      res.code +
                      "&grant_type=authorization_code",
                    success: (res) => {
                      console.log(res.data.openid);
                      // 连接后端
                      uni.request({
                        url: `http://localhost:5038/login`,
                        data: {
                          weChatOpenId: res.data.openid,
                        },
                        method: "POST",
                        success: (res) => {
                          if (res.data.code == 4000) {
                            console.log(res.data);
                          } else {
                            console.log(res.token);
                            // var userinfo = {
                            // 	photo: res
                            // 		.data
                            // 		.wechardata
                            // 		.avatarUrl,
                            // 	nickName: res
                            // 		.data
                            // 		.wechardata
                            // 		.userName,
                            // 	data: res
                            // 		.data
                            // }
                            // app.globalData =
                            // 	userinfo

                            // uni.redirectTo({
                            // 	url: '/pages/index/index'
                            // })
                          }
                        },
                      });
                    },
                  });
                },
              });
            },
          });
        } else {
          // 用户没有授权
          // 改变 isHide 的值，显示授权页面
          that.setData({
            isHide: true,
          });
        }
      },
    });
  },
  methods: {
    bindGetUserInfo: function (e) {
      var that = this;
      uni.getUserProfile({
        desc: "完善信息",
        success: (res) => {
          // this.date.AvatarUrl = res.userInfo.avatarUrl;
          // this.date.UserName = res.userInfo.nickName;
          uni.login({
            success: (res) => {
              uni.request({
                url:
                  "https://api.weixin.qq.com/sns/jscode2session?appid=wxfe85c92a33a8fb07&secret=820f54f6b3547215a59cd4511be30fe3&js_code=" +
                  res.code +
                  "&grant_type=authorization_code",
                success: (res) => {
                  this.date.WeCharId = res.data.openid;
                  that.isHide = false;
                  // 授权登录后跳转到首页
                  // uni.redirectTo({
                  // 	url: '/pages/index/index'
                  // })
                },
              });
            },
          });
        },
      });
    },
    binding: function () {
      this.date.Sid = this.value;
      //与后端链接，绑定学号后跳转到首页
      uni.request({
        url: "http://localhost:5038/bind",
        data: {
          StudentCode: this.value,
          WeChatOpenId: this.weChatOpenId,
        },
        method: "POST",
        success: (res) => {
          console.log(res.data);
          if (res.data.code == 4000) {
            console.log(res.data);
          } else {
            // var userinfo={
            // 	photo:res.data.wechardata.avatarUrl,
            // 	nickName:res.data.wechardata.userName,
            // 	data:res.data
            // }
            // app.globalData=userinfo
            uni.redirectTo({
              url: "/pages/index/index",
            });
          }
        },
      });
    },
  },
};
</script>

<style>
.header {
  margin: 90rpx 0 90rpx 50rpx;
  border-bottom: 1px solid #ccc;
  text-align: center;
  width: 650rpx;
  height: 300rpx;
  line-height: 450rpx;
}

.header image {
  width: 200rpx;
  height: 200rpx;
}

.content {
  margin-left: 50rpx;
  margin-bottom: 90rpx;
}

.content text {
  display: block;
  color: #9d9d9d;
  margin-top: 40rpx;
}

.bottom {
  border-radius: 80rpx;
  margin: 70rpx 50rpx;
  font-size: 35rpx;
}

.studentnum {
  margin-top: 120px;
  margin-left: 10px;
}

.uni-subtitle {
  font-size: 20px;
}

.studentnumtwo {
  margin-top: 10px;
}
</style>
