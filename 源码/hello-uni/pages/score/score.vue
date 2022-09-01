<template>
	<view class="content">
		<!-- 学期选择 -->
		<view class="screen">
			<view class="uni-form-item uni-column">
				<picker @change="bindPickerChange" :range="array">
					<label>学期：</label>
					<label class="">{{array[index]}}</label>
				</picker>
			</view>
		</view>
		<view class="subject_score">
			<!-- 学科成绩 -->
			<view class="subject_head">
				<span class="subject_head">
					<font>{{subject}}</font>
				</span>
			</view>
			<!-- 成绩循环 -->
			<view class="scores">
				<div class='sub' v-for="item in sub" v-model="sub">
					<div class='sub_model'>
						<span class='sub_name'>{{item.courseName}}</span>
						<span class='score'>成绩：{{item.score}}</span>
					</div>
				</div>
			</view>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				array: ['大一第一学期', '大一第二学期', '大二第一学期', '大二第二学期'],
				index: 0,
				subject: '————— ◍ 学科成绩 ◍ —————',
				score: 88,
				sub: []
			};
		},
		methods: {
			bindPickerChange: function(e) {
				// var that=this;// 改变的事件名
				this.index = e.target.value // 将数组改变索引赋给定义的index变量
				this.jg = this.array[this.index] // 将array【改变索引】的值赋给定义的jg变量
				console.log(this.index)
				console.log(this.jg)
				
				if (this.index === '0') {
			
					// this.$set(this.sub,0,{courseName:"C#",credit:2,score:60,term:1})
					// console.log(this.sub)
					uni.request({
						url: 'http://localhost:5038/score',
						header: {
							Authorization: 'Bearer ' + uni.getStorageSync('accessToken')
						},
						method: "GET",
						success: res => {
							this.sub = res.data.data
						}
					})
				}else{
					this.sub=[];
					
				}
			}
		},
		created() {

			uni.request({
				url: 'http://localhost:5038/score',
				header: {
					Authorization: 'Bearer ' + uni.getStorageSync('accessToken')
				},
				method: "GET",
				success: res => {
					console.log(res)
					this.sub = res.data.data
				}
			})

		}
	};
</script>

<style>
	.screen {
		text-align: center;
		/* background-color: #4169E1; */
		font-size: 45rpx;
	}

	.subject_score {
		text-align: center;
		padding-top: 25rpx;

	}

	.subject_head {
		font-size: 20px;
		color: #4169E1;
	}

	.sub_model {

		/* 字体间距 */
		letter-spacing: 5rpx;

		color: azure;
		text-align: left;
		display: grid;
		width: 180px;
		height: 70px;
		background-color: #89c3eb;
		margin: auto;
		margin-top: 10px;
		grid-area: model;
	}

	.sub_name {
		/* line-height: 25px; */
		margin-left: 10px;
		padding-top: 10px;
	}

	.score {
		line-height: 1px;
		margin-left: 10px;
	}

	.scores {
		display: grid;
		grid-template-areas: 'model model';
	}
</style>
