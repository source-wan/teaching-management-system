<template>
	<view>
		<view class="text">
			<text>清澈的爱给清澈的闽大</text>
		</view>
		
		<view>
			<image class="car" src="../../static/work1.jpg"></image>
		</view>
		
		<view class="introduction">
			<text>作品介绍：</text>
		</view>
		
		<view class="a" style="text-indent: 2em;">
			<text>秋日的育才湖比以往的色彩都要更丰富一点，除了有蓝色的天空和蓝白色的实训楼，还有渐黄的叶片。</text>
		</view>
		
		<view class="autho">
		<view>
			<text style="margin-left: 30%;">作者：万程祥</text>
		</view>
		<view>
			<text style="margin-left: 30%;">院系：软件工程学院</text>
		</view>
		<view>
			<text style="margin-left: 30%;">班级：20级软件技术2班</text>
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