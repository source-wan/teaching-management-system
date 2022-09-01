<template>
	<view>
		<!-- 图片 -->
		<view class="avatar">
			<image :src=touxian @click="avatar"></image>
		</view>

		<view class="text">
			<table>
				<tr class="a">
					<td>
						<label style="font-size: 20px;">姓名:</label>
						<input class="b" type="text" v-model="form.name" />
					</td>
				</tr>

				<tr class="c">
					<td>
						<label style="font-size: 20px;">学号:</label>
					</td>
					<td>
						<input class="d" type="text" v-model="form.studentCode" />
					</td>
				</tr>

				<tr class="e">
					<td>
						<label style="font-size: 20px;">电话:</label>
					</td>
					<td>
						<input class="f" type="text" v-model="form.phone" />
					</td>
				</tr>

				<tr class="g">
					<td>
						<label style="font-size: 20px;">邮箱:</label>
					</td>
					<td>
						<input class="h" type="text" v-model="form.mail" />
					</td>
				</tr>
			</table>
		</view>
		<image class="ing" src="../../static/ing.jpg"></image>
	</view>


</template>
<script>
	// 图片上传
	const app = getApp()
	export default {
		data() {
			return {
				imgArr: "",
				touxian: app.globalData.photo,
				form: {
					name: "",
					account: "",
					major: "",
					class: "",
					studentCode: "",
					phone: "",
				},
			}
		},
		methods: {

		},
		created() {
			var that = this;
			let studentCode = app.globalData.data.info.data.updated.id
			uni.request({
				url: `http://localhost:5038/student?userId=${studentCode}`,
				header: {
					Authorization: 'Bearer ' + uni.getStorageSync('accessToken')
				},
				method: "GET",
				success: res => {
					console.log(res);
					that.form.name = res.data.data.data[0].name
					that.form.studentCode = res.data.data.data[0].studentCode
					that.form.mail = res.data.data.data[0].mail
					that.form.phone = res.data.data.data[0].phone
						
				}
			})

		}
	}
</script>

<style>
	/* 底部图片 */
	.ing {
		margin-left: -1px;
		margin-top: 350px;
		width: 100%;
		height: 500px;
	}

	// 头像背景设置
	.avatar {
		background-color: #4169E1;
		width: 100%;
		height: 200px;
	}

	// 头像
	.avatar>image {
		border-radius: 50%;
		height: 100px;
		width: 100px;
		margin-left: 35%;
		margin-top: 50px;
	}

	/* 上传头像 */
	.button {
		margin-top: 160px;
		margin-left: 30px;
		width: 100px;
		height: 30px;
	}

	/* 图片 */
	image {
		margin-top: -2px;
		margin-left: 30px;
		width: 100px;
		height: 100px;
	}

	.a {
		position: absolute;
		top: 220px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;
		size: 20rpx;
	}

	.b {
		position: relative;
		left: 55px;
		bottom: 23px;
		font-size: 20px;
	}

	.c {

		position: absolute;
		top: 260px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;
	}

	.d {
		position: relative;
		left: 55px;
		bottom: 23px;
		font-size: 20px;
	}

	.e {

		position: absolute;
		top: 300px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;
	}

	.f {
		position: relative;
		left: 55px;
		bottom: 23px;
		font-size: 20px;

	}

	.g {

		position: absolute;
		top: 340px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;
	}

	.h {
		position: relative;
		left: 55px;
		bottom: 23px;
		font-size: 20px;
	}

	.i {

		position: absolute;
		top: 380px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;
	}

	.j {
		position: relative;
		left: 80px;
		bottom: 23px;
		font-size: 20px;

	}

	.k {

		position: absolute;
		top: 420px;
		/* 字体间距 */
		letter-spacing: 5rpx;
		margin-top: 10px;

	}

	.l {
		position: relative;
		left: 80px;
		bottom: 23px;
		font-size: 20px;

	}

	.text {
		margin-left: 100px;
	}
</style>
