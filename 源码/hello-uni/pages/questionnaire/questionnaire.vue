<template>
	<view class="problem" v-model="formLabelAlign">
		<view class="problem_top">
			<view class='title'>
				<span>{{formLabelAlign.title}}</span>
			</view>
			<view class='publish'>
				<span>发布人：{{formLabelAlign.publisher}}</span><br>
				<span>发布时间：{{formLabelAlign.publishTime}}</span>
			</view>

		</view>
		<view class="question" v-model="questions">
			<view v-for="item in questions">
				<view class="ques_title">
					<span>{{item.id}} 、{{item.title}}</span>
				</view>
				<view>
					<slider value="1" @change="sliderChange" min="1" max="10" show-value />
				</view>
			</view>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				formLabelAlign: {
					title: '专业课满意度调查',
					publisher: '老胡来也',
					publishTime: "2022/08/15",

				},
				questions: [{
						id: 1,
						title: '老师讲课对重难点把握'
					},
					{
						id: 2,
						title: '老师对课后练习讲解情况'
					},
					{
						id: 3,
						title: '老师对上课玩手机、睡觉现象及时提醒情况'
					},

				]
			}
		},
		methods: {
			sliderChange(e) {
				console.log('value 发生变化：' + e.detail.value)
			}
		}
	}
</script>

<style>
	* {
		background-color: #BEBEBE;
	}

	.problem {
		background-color: #f0f2f5;
		padding-top: 30rpx;
	}

	.problem_top {
		background-color: white;
		height: 300rpx;
		float: left;
		width: 100%;
	}


	.title {
		font-size: 45rpx;
		color: blue;
		text-align: center;
		margin-top: 80rpx;
	}

	.publish {
		text-align: center;
		margin-top: 15rpx;
		font-size: 30rpx;
	}

	.question {
		margin-top: 15rpx;
		width: 100%;
		background-color: white;
		line-height: 45rpx;
		margin-bottom: 20rpx;
		float: left;
	}

	.ques_title {
		background-color: #f0f2f5;
	}
</style>
