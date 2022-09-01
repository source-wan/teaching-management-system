<template>
	<view>
		<view class="weui-search-bar">
			<view class="weui-search-bar__form">
				<!-- 搜索框 -->
				<view class="weui-search-bar__box">
					<icon class="weui-icon-search_in-box" type="search" size="14"></icon>
					<input type="text" class="weui-search-bar__input" placeholder="请输入搜索内容" />
				</view>
			</view>
			<!-- 搜索按钮,调用搜索查询方法 -->
			<view class="weui-search-bar__cancel-btn" bindtap='方法名a'>搜索</view>
		</view>
		
		<!-- 卡片 -->
		<view>
			<uni-card :isShadow="true" title="20软件技术2班——袁子" mode="basic" extra="摄影大赛一等奖" @click="clickCard">
				<view>
					<view style="height: 350rpx;">
						<image class="img" mode="aspectFill"
							src="https://vkceyugu.cdn.bspapp.com/VKCEYUGU-dc-site/094a9dc0-50c0-11eb-b680-7980c8a877b8.jpg" />
					</view>
				</view>
			</uni-card>
		</view>

		<view>
			<uni-card :isShadow="true" title="20软件技术2班——袁子" mode="basic" extra="摄影大赛一等奖" @click="click()">
				<view>
					<view style="height: 350rpx;">
						<image class="img" mode="aspectFill"
							src="https://vkceyugu.cdn.bspapp.com/VKCEYUGU-dc-site/094a9dc0-50c0-11eb-b680-7980c8a877b8.jpg" />
					</view>
				</view>
			</uni-card>
		</view>

		<view>
			<uni-card :isShadow="true" title="20软件技术2班——袁子" mode="basic" extra="摄影大赛一等奖" @click="video()">
				<view>
					<video id="myVideo" class="video"
						src="https://img.cdn.aliyun.dcloud.net.cn/guide/uniapp/%E7%AC%AC1%E8%AE%B2%EF%BC%88uni-app%E4%BA%A7%E5%93%81%E4%BB%8B%E7%BB%8D%EF%BC%89-%20DCloud%E5%AE%98%E6%96%B9%E8%A7%86%E9%A2%91%E6%95%99%E7%A8%8B@20200317.mp4"
						@error="videoErrorCallback" :danmu-list="danmuList" enable-danmu danmu-btn controls></video>
				</view>
			</uni-card>
		</view>

	</view>
</template>



<script>
	// 引入卡片组件
	import core from '@/pages/works/works.vue'

	export default {
		components: {
			core
		},
		data() {
			return {
			};
		},
		methods: {
			clickCard: function() {
				uni.navigateTo({
					url: '/pages/details/details'
				})
			},
			click: function() {
				uni.navigateTo({
					url: '/pages/detailss/detailss'
				})
			},
			video: function() {
				uni.navigateTo({
					url: '/pages/detailsss/detailsss'
				})
			},
		}
	};
</script>


<style>
/* 视频 */
.video{
	width: 100%;
}

	/* 图片 */
	.img {
		width: 100%;
	}

	/* 搜索栏 */
	.weui-search-bar {
		position: relative;
		padding: 10px 20px;
		display: -webkit-box;
		display: -webkit-flex;
		display: flex;
		box-sizing: border-box;
		background-color: #EFEFF4;
		border-top: 40px solid #D7D6DC;
		border-bottom: 1rpx solid #D7D6DC;
	} 
	.weui-icon-search_in-box {
		position: absolute;
		left: 10px;
		top: 7px;
	}

	.weui-search-bar__form {
		position: relative;
		-webkit-box-flex: 1;
		-webkit-flex: auto;
		flex: auto;
		border-radius: 5px;
		background: #FFFFFF;
		border: 1rpx solid #E6E6EA;
	}

	.weui-search-bar__box {
		position: relative;
		padding-left: 30px;
		padding-right: 30px;
		width: 100%;
		box-sizing: border-box;
		z-index: 1;
	}

	.weui-search-bar__input {
		height: 28px;
		line-height: 28px;
		font-size: 14px;
	}

	.weui-search-bar__cancel-btn {
		margin-left: 10px;
		line-height: 28px;
		color: #09BB07;
		white-space: nowrap;
	}
</style>
