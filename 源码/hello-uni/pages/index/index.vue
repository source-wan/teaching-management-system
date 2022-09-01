<template>
	<view>
		<view class="uni-margin-wrap">
			<swiper class="swiper" circular :indicator-dots="indicatorDots" :autoplay="autoplay" :interval="interval"
				:duration="duration">
				<swiper-item>
					<image class="logo" src="../../static/2.jpg"></image>
				</swiper-item>
				<swiper-item>
					<image class="logo" src="../../static/3.jpg"></image>
				</swiper-item>
				<swiper-item>
					<image class="logo" src="../../static/4.jpg"></image>
				</swiper-item>
				<swiper-item>
					<image class="logo" src="../../static/5.jpg"></image>
				</swiper-item>
				<swiper-item>
					<image class="logo" src="../../static/6.jpg"></image>
				</swiper-item>
			</swiper>
		</view>
		<view class="my_view">
			
			<view class="my_view_text" @click="grade">
				<view class="a">
				<image class="avatar" src="../../static/c.png"></image>
				</view>
				
				<text class="my_view_position" style="color: #4169E1;size: 50px;">成绩查找</text>
			</view>

			<view class="my_view_text" @click="questionnaire">
				<view class="a">
				<image class="avatar" src="../../static/b.png"></image>
				</view>
				
				<text class="my_view_position" style="color: #4169E1;">调查问卷</text>
			</view>

			<view class="my_view_text" @click="setUp">
				<view class="a">
				<image class="avatar" src="../../static/d.png"></image>
				</view>
				
				<text class="my_view_position" style="color: #4169E1;size: 50px;">优秀作品</text>
			</view>
			
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				indicatorDots: true,
				autoplay: true,
				// 自动播放间隔时长
				interval: 2000,
				// 幻灯片切换时长
				duration: 500
			}
		},
		methods: {
			// 优秀作品
			setUp: function() {
				uni.navigateTo({
					url: '/pages/works/works'
				})
			},
			// 成绩查找
			grade:function(){
				uni.navigateTo({
					url:'/pages/score/score'
				})
			},
			// 问卷调查
			questionnaire:function(){
				uni.navigateTo({
					url:'/pages/question/question'
				})
			}
			
		}
	}
</script>


<style>
	/* 箭头图片 */
	.right{
		width:40px;
		height:40px;
		margin-left: 70px;
	}
	
	/* 字体 */
	.my_view_position{
		font-size: 20px;
		margin-left: 100rpx;
	}


	/* 模块 */
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

	/* 轮播图 */
	.logo {
		width: 100%;
		margin: 2rpx 0;
		text-align: center;

	}

	/* 图标 */
	.avatar{
		width: 90px;
		height: 90px;
		z-index: 9;
		margin-left: 50rpx;
	}

	.swiper {
		height: 500rpx;
	}
</style>
