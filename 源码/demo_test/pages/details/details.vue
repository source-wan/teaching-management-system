<template>
	<view>
		<view class="text">
			<text>微信小程序，小程序代理光速云小程序</text>
		</view>
		
		<view>
			<image class="car" src="../../static/avatar.jpg"></image>
		</view>
		
		<view class="introduction">
			<text>作品介绍：</text>
		</view>
		
		<view class="a" style="text-indent: 2em;">
			<text>小程序目前支持内嵌网页，所以之前很多在APP里操作的项目都能搬到小程序当中来。比如小程序算命、风水、星座等项目，虽然简单但是很有趣受到年轻人的喜爱，基于广大的微信用户群体，裂变速度其实是非常迅速地。</text>
		</view>
		
		<view class="autho">
		<view>
			<text style="margin-left: 50%;">作者：袁子</text>
		</view>
		<view>
			<text style="margin-left: 50%;">院系：软件工程学院</text>
		</view>
		<view>
			<text style="margin-left: 50%;">班级：软件二班</text>
		</view>
		</view>
		
	</view>
</template>

<script>
</script>

<style>
	/* 标题 */
	.text{
		/* 字体间距 */
		/* color: crimson; */
		letter-spacing:5rpx;
		font-size: 20px;
		margin-top: 10px;
		text-align: center;
	}
		/* 作品介绍 */
	.introduction{
		/* 字体间距 */
		letter-spacing:5rpx;
		margin-top: 30px;
		font-size: 20px;
	}
	
	/* 卡片组件 */
	.car{
		margin-top: 20px;
		width: 100%;
		height: 300px;
	}
		
	.a{
		/* 字体间距 */
		letter-spacing:5rpx;
		font-size: 20px;
	}
	
	.autho{
		/* 字体间距 */
		letter-spacing:5rpx;
		margin-top: 30px;
		font-size: 20px;
	}
</style>