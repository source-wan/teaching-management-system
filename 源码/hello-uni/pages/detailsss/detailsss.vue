<template>
	<view>
		<view class="text">
			<text>微信小程序，小程序代理光速云小程序</text>
		</view>
		
		<view>
			<video id="myVideo" class="video"
				src="https://img.cdn.aliyun.dcloud.net.cn/guide/uniapp/%E7%AC%AC1%E8%AE%B2%EF%BC%88uni-app%E4%BA%A7%E5%93%81%E4%BB%8B%E7%BB%8D%EF%BC%89-%20DCloud%E5%AE%98%E6%96%B9%E8%A7%86%E9%A2%91%E6%95%99%E7%A8%8B@20200317.mp4"
				@error="videoErrorCallback" :danmu-list="danmuList" enable-danmu danmu-btn controls>
			</video>
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
	
	/* 视频 */
	.video{
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