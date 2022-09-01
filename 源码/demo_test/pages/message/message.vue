<template>
	<view class="major">
		<view class="avatar">
			<!-- 头像 -->
			<image src="../../static/a.webp" @click="avatar"></image>
		</view>
		<view class="my_view">
			<view class="my_view_text" @click="myMessege">
				<image src="../../static/message.png"></image>
				<text class="my_view_position" style="color: #4169E1;size: 50px;">个人资料</text>
			</view>
			<view class="my_view_text" @click="about">
				<image src="../../static/summarized.png"></image>
				<text class="my_view_position" style="color: #4169E1;">学校概况</text>
			</view>
			<view class="my_view_text" @click="logout">
				<image src="../../static/logout.png"></image>
				<text class="my_view_position" style="color: #4169E1;">退出登录</text>
			</view>
			<!-- <view class="my_view_text" @click="setUp">
				<image src="../../static/a.webp"></image>
				<text class="my_view_position">设置</text>
			</view> -->
		</view>、
	</view>
</template>

<script>
		
	export default{
		methods:{
			// 个人资料
			myMessege:function(){
				uni.navigateTo({
					// 点击跳转的路径
					url:'/pages/people/people'
				})
			},
			// 学校概况
			about:function(){
				uni.navigateTo({
					url:'/pages/about/about '
				})
			},
			// 退出登录
			logout:function(){
				uni.navigateTo({
					url:''
				})
			},
			// 更换头像（暂时无法使用）
			avatar:function(){
				let id=uni.getAccountInfoSync('user').id
				uni.chooseImage({//从本地相册选择图片或使用相机拍照
					count:1,//默认选择图片1张，最多可传9张
					sourceType:['album','camera'],//从相册选择,和摄像头功能，默认二者都有
					sizeType:['original','compressed'],//original 原图，compressed压缩图，默认二者都有
					success:(res)=>{
						
						console.log(res.tempFilePaths[0],'头像res'),//成功则返回图片的本地文件路径列表
						this.userInfo.headimg=res.tempFilePaths[0]//更新本地浏览头像图片
						this.update(res.tempFilePaths[0])//上传图片
					}
				})
			}
		}
	}
	
</script>

<style lang="scss">

	
	// 头像背景设置
	.avatar{
		background-color: #4169E1;
		height: 200px;
	}
	
	// 头像
	.avatar>image{
		border-radius: 50%;
		height:100px;
		width: 100px;
		margin-left: 35%;
		margin-top: 50px;
	}
	
	// 模块
	.my_view_text{
		margin: 100px;
		width: 96%;
		height: 29vw;
		background-color: #fefefe;
		border-radius: 24upx;
		box-shadow: 0 0 20upx rgba(0, 0, 0, 0.15);
		margin: 40upx 2% 0upx 2%;
		display: flex;
		align-items: center;
	}
	
	// 模块里的图片
	image{
		width: 90px;
		height: 90px;
		z-index: 9;
		margin-left: 50rpx;
	}
	
	// 字体
	.my_view_position{
		font-size: 20px;
		margin-left: 100rpx;
	}
</style>