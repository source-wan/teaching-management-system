<template>
	<view class="question">
		<div class="ques_top">
			<div class="my_ques">
				<span>我的问卷</span>
			</div>
			<div class="ques_unfinished">
				<span>{{no_complete}}</span><br>
				<span>未完成问卷</span>
			</div>
		</div>
		<view class="ulist">
			<view
				:class="{active:active==index}" 
				@click="complete(index)" 
				v-for="(item,index) in performance" 
				:key="index"
			>
				{{item}}</view>
		</view>
		<!-- v-if判断方法判断active值 点击跳转到对应的页面 -->
		<view class="" v-if="active == 0">
			<view class="">
				<button @click="proceed">
					<uni-section class="mb-10" title="专业课满意度调查" sub-title="老胡" type="line"></uni-section>
				</button>
			</view>
		</view>
		<view class="" v-if="active == 1">
			222
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				active: 0,
				no_complete:0,
				performance: ['进行中问卷', '已完成问卷']
			}
		},
		methods: {
			complete(id) {  // 判断id 在哪个页面
				this.active = id
			},
			proceed(){
				console.log('进行问卷调查');
				uni.navigateTo({
					url:'/pages/questionnaire/questionnaire'
				})
			}
		}
	}
</script>

<style lang="scss">
	$uni-success: #18bc37 !default;
	.ques_top {
		width: 100%;
		height: 260rpx;
		background-color: #DCDCDC;
		float: left;
		display: flex;
	}

	.my_ques,
	.ques_unfinished {
		width: 50%;
		text-align: center;
		height: 260rpx;
		float: left;
		margin: auto;
	}

	.my_ques {
		margin-top: 10%;
		font-size: 75rpx;
		color: white;
	}

	.ques_unfinished {
		font-size: 45rpx;
		line-height: 50rpx;
		color: white;
		margin-top: 10%;
	}
	
	/* 切换 */
	.ulist{
		display: flex;
		justify-content: space-around;
		font-size: 40rpx;
		padding-top: 20rpx;
		padding-bottom: 20rpx;
		width: 100%;
		background-color: #fff;
		color: black;
	}
	
	.ulist .active{
		color: aqua;
		padding: 20rpx;
		border-bottom: 4rpx soild black;
	}
</style>
